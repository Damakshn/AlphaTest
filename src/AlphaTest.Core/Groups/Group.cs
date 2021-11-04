using System;
using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Users;
using AlphaTest.Core.Groups.Rules;

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
            CheckCommonRulesForDates(beginDate, endDate);
            CheckRule(new GroupNameMustBeProvidedRule(name));
            // ToDo unit test
            if (curator is not null)
                CheckRule(new OnlyTeacherCanBeAssignedAsCurator(curator));
            ID = Guid.NewGuid();
            Name = name;
            BeginDate = beginDate;
            EndDate = endDate;
            IsDisbanded = false;
            _members = new List<Membership>();
            CuratorID = curator?.ID;
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
        public bool IsGone => EndDate <= DateTime.Now;
        #endregion

        #region Методы

        #region Редактирование
        public void AddStudent(IAlphaTestUser student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new GroupSizeIsLimitedRule(_members));
            Membership membership = new(this, student);
            _members.Add(membership);
            // ToDo domain event
        }

        public void ExcludeStudent(IAlphaTestUser student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new NonMemberStudentsCannotBeExcludedFromGroupRule(this, student));
            Membership membershipToRemove = _members.Where(m => m.StudentID == student.ID).FirstOrDefault();
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
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckCommonRulesForDates(newBegin, newEnd);
            BeginDate = newBegin;
            EndDate = newEnd;
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
            CuratorID = curator.ID;
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
