using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Users;


namespace AlphaTest.Core.Groups.Rules
{
    public class NonMemberStudentsCannotBeExcludedFromGroupRule : IBusinessRule
    {
        private readonly Group _group;

        private readonly IAlphaTestUser _studentToExclude;

        public NonMemberStudentsCannotBeExcludedFromGroupRule(Group group, IAlphaTestUser studentToExclude)
        {
            _group = group;
            _studentToExclude = studentToExclude;
        }

        public string Message => "Нельзя исключить учащегося из группы, так как он в ней не состоит.";

        public bool IsBroken => _group.Memberships.Count(m => m.StudentID == _studentToExclude.Id) == 0;
    }
}
