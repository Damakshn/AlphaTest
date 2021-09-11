using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Groups.Rules;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups
{
    public class Membership: Entity
    {
        private Membership() { }

        internal Membership(Group group, User student)
        {
            CheckRule(new OnlyStudentsCanBeIncludedIntoGroupRule(student));
            CheckRule(new SuspendedUsersCannotBeIncludedIntoGroupRule(student));
            GroupID = group.ID;
            StudentID = student.ID;
        }

        public int GroupID { get; private set; }

        public int StudentID { get; private set; }
    }
}
