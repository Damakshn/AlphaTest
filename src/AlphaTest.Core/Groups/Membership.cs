using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Groups.Rules;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups
{
    public class Membership: Entity
    {
        private Membership() { }

        internal Membership(Group group, IAlphaTestUser student)
        {
            CheckRule(new OnlyStudentsCanBeIncludedIntoGroupRule(student));
            CheckRule(new SuspendedUsersCannotBeIncludedIntoGroupRule(student));
            GroupID = group.ID;
            StudentID = student.Id;
        }

        public Guid GroupID { get; private set; }

        public Guid StudentID { get; private set; }
    }
}
