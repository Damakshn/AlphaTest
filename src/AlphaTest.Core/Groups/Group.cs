using System;
using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Users;
using AlphaTest.Core.Groups.Rules;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Core.Groups
{
    public class Group: Entity
    {
        #region Поля
        private List<Membership> _members;
        #endregion

        #region Конструкторы
        private Group() { }

        public Group(string name, DateTime beginDate, DateTime endDate, IAlphaTestUser curator, bool groupAlreadyExists)
        {
            CheckRule(new GroupMustBeUniqueRule(groupAlreadyExists));
            DateTime beginDateUtc = TimeResolver.ToUtc(beginDate);
            DateTime endDateUtc = TimeResolver.ToUtc(endDate);
            CheckCommonRulesForDates(beginDateUtc, endDateUtc);
            CheckRule(new GroupNameMustBeProvidedRule(name));
            // ToDo unit test
            if (curator is not null)
                CheckRule(new OnlyTeacherCanBeAssignedAsCurator(curator));
            ID = Guid.NewGuid();
            Name = name;
            BeginDate = beginDateUtc;
            EndDate = endDateUtc;
            IsDisbanded = false;
            _members = new List<Membership>();
            CuratorID = curator?.Id;
        }
        #endregion

        #region Свойства
        public Guid ID { get; private set; }

        public string Name { get; private set; }

        public DateTime BeginDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public IReadOnlyCollection<Membership> Memberships => _members.AsReadOnly();

        public bool IsDisbanded { get; private set; }

        public Guid? CuratorID { get; private set; }

        // MAYBE придумать другое название
        public bool IsGone => EndDate <= TimeResolver.CurrentTime;
        #endregion

        #region Методы

        #region Редактирование
        public void AddStudent(IAlphaTestUser student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new StudentCanBeAddedToGroupOnlyOnceRule(this, student));
            CheckRule(new GroupSizeIsLimitedRule(_members));
            Membership membership = new(this, student);
            _members.Add(membership);
            // ToDo domain event
        }

        public bool HasMember(IAlphaTestUser student)
        {
            return Memberships.Any(m => m.StudentID == student.Id);
        }

        public void ExcludeStudent(IAlphaTestUser student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new NonMemberStudentsCannotBeExcludedFromGroupRule(this, student));
            Membership membershipToRemove = _members.Where(m => m.StudentID == student.Id).FirstOrDefault();
            _members.Remove(membershipToRemove);
            // ToDo domain event
        }

        public void Disband()
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            IsDisbanded = true;
        }

        public void Restore()
        {   
            IsDisbanded = false;
        }

        // ToDo согласованность с расписанием экзаменов
        public void ChangeDates(DateTime newBegin, DateTime newEnd)
        {
            DateTime newBeginUtc = TimeResolver.ToUtc(newBegin);
            DateTime newEndUtc = TimeResolver.ToUtc(newEnd);
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckCommonRulesForDates(newBeginUtc, newEndUtc);
            BeginDate = newBeginUtc;
            EndDate = newEndUtc;
        }

        // TBD - критерии уникальности группы, требуется правка документации
        public void Rename(string newName, bool groupAlreadyExists)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new GroupMustBeUniqueRule(groupAlreadyExists));
            CheckRule(new GroupNameMustBeProvidedRule(newName));
            Name = newName;
        }

        public void AssignCurator(IAlphaTestUser curator)
        {
            // Todo unit test
            CheckRule(new CuratorMustBeProvidedForAssignmentRule(curator));
            CheckRule(new OnlyTeacherCanBeAssignedAsCurator(curator));
            CuratorID = curator.Id;
        }

        public void UnsetCurator()
        {
            // Todo unit test
            CuratorID = null;
        }
        #endregion

        #region Сгруппированные проверки
        private void CheckCommonRulesForDates(DateTime beginDate, DateTime endDate)
        {
            CheckRule(new GroupCannotBeCreatedInThePastRule(beginDate));
            CheckRule(new GroupEndDateMustFollowBeginDateRule(beginDate, endDate));
        }

        #endregion
        #endregion
    }
}
