using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Core.Users.BulkImportReport;
using AlphaTest.Core.Common.Exceptions;
using AlphaTest.Core.Groups.Rules;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.StudentBulkImport
{
    public class BulkImportUseCaseHandler : UseCaseHandlerBase<BulkImportUseCaseRequest, List<BulkImportReportLine>>
    {
        #region Вспомогательные поля
        private List<Group> _outdatedGroups;
        private List<Group> _disbandedGroups;
        private List<Group> _groupsToPopulate;
        private List<Guid> _missingGroupsIDs;
        private List<ImportStudentRequestData> _importedUsersWithOutdatedGroup;
        private List<ImportStudentRequestData> _importedUsersWithDisbandedGroup;
        private List<ImportStudentRequestData> _newUsersToImport;
        private List<AppUser> _existingValidUsers;
        private List<AppUser> _undistributedStudents;
        #endregion
        private List<BulkImportReportLine> _reportLines;
        private readonly UserManager<AppUser> _userManager;

        public BulkImportUseCaseHandler(AlphaTestContext db, UserManager<AppUser> userManager) : base(db)
        {
            _userManager = userManager;
            _undistributedStudents = new List<AppUser>();
            _reportLines = new List<BulkImportReportLine>();
        }

        public override async Task<List<BulkImportReportLine>> Handle(BulkImportUseCaseRequest request, CancellationToken cancellationToken)
        {
            await PreprocessRequestData(request);
            await SaveImportedUsersAsStudents();
            await AddExistingUsersToStudentsIfNeeded();
            DistributeStudentsIntoGroups(request);
            ReportLosers(request);
            _db.SaveChanges();
            return _reportLines;

        }

        private async Task PreprocessRequestData(BulkImportUseCaseRequest request)
        {
            List<Guid> groupIds = request.Students.Select(s => s.GroupID).ToList();
            List<Group> existingGroups = await _db.Groups.Aggregates().FilterByIdsList(groupIds).ToListAsync();
            _missingGroupsIDs = groupIds.Except(existingGroups.Select(g => g.ID).ToList()).ToList();
            _disbandedGroups = existingGroups.Where(g => g.IsDisbanded).ToList();
            _outdatedGroups = existingGroups.Where(g => g.IsGone).ToList();
            _groupsToPopulate = existingGroups.Except(_disbandedGroups).Except(_outdatedGroups).ToList();

            _importedUsersWithOutdatedGroup = request.Students.Where(s => _outdatedGroups.Any(g => g.ID == s.GroupID)).ToList();
            _importedUsersWithDisbandedGroup = request.Students.Where(s => _disbandedGroups.Any(g => g.ID == s.GroupID)).ToList();

            List<string> validStudentsEmails =
                request.Students.Except(_importedUsersWithOutdatedGroup).Except(_importedUsersWithDisbandedGroup).Select(s => s.Email).ToList();

            _existingValidUsers = await _db.Users.Aggregates().FilterByEmailsList(validStudentsEmails).ToListAsync();

            _newUsersToImport = request
                .Students
                .Except(_importedUsersWithOutdatedGroup)
                .Except(_importedUsersWithDisbandedGroup)
                .Where(s => !_existingValidUsers.Any(u => u.Email == s.Email))
                .ToList();
        }

        private async Task SaveImportedUsersAsStudents()
        {
            foreach (var studentInfo in _newUsersToImport)
            {
                string reportContent;
                BulkImportEventType eventType;
                try
                {
                    string temporaryPassword = PasswordGenerator.GeneratePassword(SecuritySettings.PasswordOptions);
                    AppUser newStudent = new(
                            studentInfo.FirstName,
                            studentInfo.LastName,
                            studentInfo.MiddleName,
                            temporaryPassword,
                            studentInfo.Email);
                    await _userManager.CreateAsync(newStudent);
                    await _userManager.AddToRoleAsync(newStudent, "Student");
                    _undistributedStudents.Add(newStudent);
                    reportContent = $"Данные об учащемся импортированы - {studentInfo.LastName} {studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.Email}";
                    eventType = BulkImportEventType.NewStudentSuccessfullyImported;
                }
                catch (Exception)
                {
                    // ToDo manage exception, include exception info into report
                    reportContent = $"Ошибка импорта данных об учащемся - {studentInfo.LastName} {studentInfo.FirstName} {studentInfo.MiddleName} {studentInfo.Email}";
                    eventType = BulkImportEventType.UnknownError;
                }
                BulkImportReportLine reportLine = new(reportContent, eventType);
                _reportLines.Add(reportLine);
            }
        }

        private async Task AddExistingUsersToStudentsIfNeeded()
        {
            foreach (AppUser existingUser in _existingValidUsers)
            {
                string reportContent;
                BulkImportEventType eventType;
                if (!existingUser.IsStudent)
                {
                    try
                    {
                        await _userManager.AddToRoleAsync(existingUser, "Student");
                        reportContent = $"{existingUser.LastName} {existingUser.FirstName} {existingUser.MiddleName} ({existingUser.Email}) зарегистрирован как учащийся.";
                        eventType = BulkImportEventType.ExistingUserSuccessfullyAddedToStudents;
                        _undistributedStudents.Add(existingUser);
                    }
                    catch (Exception)
                    {
                        reportContent = $"Ошибка предоставления доступа - {existingUser.LastName} {existingUser.FirstName} {existingUser.MiddleName} ({existingUser.Email})";
                        eventType = BulkImportEventType.UnknownError;
                    }
                    BulkImportReportLine reportLine = new(reportContent, eventType);
                    _reportLines.Add(reportLine);
                }
                else
                {
                    _undistributedStudents.Add(existingUser);
                }
                
            }
        }

        private void DistributeStudentsIntoGroups(BulkImportUseCaseRequest request)
        {
            var data = from student in _undistributedStudents
                       join importInfo in request.Students on student.Email equals importInfo.Email
                       join groupToPopulate in _groupsToPopulate on importInfo.GroupID equals groupToPopulate.ID
                       select new { Student = student, Group = groupToPopulate };
            foreach (var item in data)
            {
                string reportContent;
                BulkImportEventType eventType;
                if (item.Group.HasMember(item.Student) == false)
                {
                    item.Group.AddStudent(item.Student);
                    reportContent = $"Учащийся {item.Student.LastName} {item.Student.LastName} {item.Student.LastName} добавлен в группу {item.Group.Name}";
                    eventType = BulkImportEventType.StudentSuccessfullyAddedToGroup;
                }
                else
                {
                    reportContent = $"Учащийся {item.Student.LastName} {item.Student.LastName} {item.Student.LastName} уже состоит в группе {item.Group.Name}";
                    eventType = BulkImportEventType.StudentAlreadyInGroup;
                }
                _reportLines.Add(new BulkImportReportLine(reportContent, eventType));
            }
        }

        private void ReportLosers(BulkImportUseCaseRequest request)
        {
            ReportMissingGroups(request);
            ReportOutdatedGroups(request);
            ReportDisbandedGroups(request);
        }

        private void ReportMissingGroups(BulkImportUseCaseRequest request)
        {
            var missingGroupsData = from missingGroupID in _missingGroupsIDs
                                    join importInfo in request.Students on missingGroupID equals importInfo.GroupID
                                    select importInfo;
            foreach (ImportStudentRequestData item in missingGroupsData)
            {
                string reportContent = $"Ошибка импорта - {item.LastName} {item.FirstName} {item.MiddleName} ({item.Email}) - группа не найдена.";
                _reportLines.Add(new(reportContent, BulkImportEventType.ImportErrorGroupNotFound));
            }
        }

        private void ReportOutdatedGroups(BulkImportUseCaseRequest request)
        {
            var outdatedGroupsData = from outdatedGroup in _outdatedGroups
                                     join importInfo in request.Students on outdatedGroup.ID equals importInfo.GroupID
                                     select importInfo;
            foreach (ImportStudentRequestData item in outdatedGroupsData)
            {
                string reportContent = $"Ошибка импорта - {item.LastName} {item.FirstName} {item.MiddleName} ({item.Email}) - срок существования группы истёк.";
                _reportLines.Add(new(reportContent, BulkImportEventType.ImportErrorGroupIsOutdated));
            }
        }

        private void ReportDisbandedGroups(BulkImportUseCaseRequest request)
        {
            var disbandedGroupsData = from disbandedGroup in _disbandedGroups
                                      join importInfo in request.Students on disbandedGroup.ID equals importInfo.GroupID
                                      select importInfo;
            foreach (ImportStudentRequestData item in disbandedGroupsData)
            {
                string reportContent = $"Ошибка импорта - {item.LastName} {item.FirstName} {item.MiddleName} ({item.Email}) - группа расформирована.";
                _reportLines.Add(new(reportContent, BulkImportEventType.ImportErrorGroupIsDisbanded));
            }
        }


    }
}
