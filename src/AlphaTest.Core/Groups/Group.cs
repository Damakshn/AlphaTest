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

        public Group(int id, string name, DateTime beginDate, DateTime endDate, bool groupAlreadyExists)
        {   
            CheckRule(new GroupMustBeUniqueRule(groupAlreadyExists));
            CheckRule(new GroupCannotBeCreatedInThePastRule(beginDate));
            CheckRule(new GroupEndDateMustFollowBeginDateRule(beginDate, endDate));
            CheckRule(new GroupNameMustBeProvidedRule(name));
            ID = id;
            Name = name;
            BeginDate = beginDate;
            EndDate = endDate;
            IsDisbanded = false;
            _members = new List<Membership>();
        }
        #endregion

        #region Свойства
        public int ID { get; private set; }

        public string Name { get; private set; }

        public DateTime BeginDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public IReadOnlyCollection<Membership> Memberships => _members.AsReadOnly();

        public bool IsDisbanded { get; private set; }

        public bool IsActive => BeginDate < DateTime.Now && EndDate > DateTime.Now;
        #endregion

        #region Методы
        public void AddStudent(User student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            CheckRule(new GroupSizeIsLimitedRule(_members));
            Membership membership = new(this, student);
            _members.Add(membership);
            // ToDo domain event
        }

        public void RemoveStudent(User student)
        {
            CheckRule(new DisbandedGroupCannotBeModifiedRule(this));
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            Membership membershipToRemove = _members.Where(m => m.StudentID == student.ID).FirstOrDefault();
            _members.Remove(membershipToRemove);
            // ToDo domain event
        }

        public void Disband()
        {
            CheckRule(new InactiveGroupCannotBeModifiedRule(this));
            IsDisbanded = true;
        }

        public void Restore()
        {
            IsDisbanded = false;
        }

        public void ChangeDates(DateTime newBegin, DateTime newEnd)
        {
            // todo checks
            BeginDate = newBegin;
            EndDate = newEnd;
        }
        #endregion
    }
}
