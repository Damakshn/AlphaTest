using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    public class OnlyStudentsCanBeIncludedIntoGroupRule : IBusinessRule
    {
        private readonly User _candidate;

        public OnlyStudentsCanBeIncludedIntoGroupRule(User candidate)
        {
            _candidate = candidate;
        }

        public string Message => "В состав группы могут входить только студенты.";

        public bool IsBroken => _candidate.IsInRole(UserRole.STUDENT) == false;
    }
}
