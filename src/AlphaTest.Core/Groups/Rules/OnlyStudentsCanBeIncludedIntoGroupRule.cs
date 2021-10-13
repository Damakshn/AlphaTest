using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    public class OnlyStudentsCanBeIncludedIntoGroupRule : IBusinessRule
    {
        private readonly IAlphaTestUser _candidate;

        public OnlyStudentsCanBeIncludedIntoGroupRule(IAlphaTestUser candidate)
        {
            _candidate = candidate;
        }

        public string Message => "В состав группы могут входить только студенты.";

        public bool IsBroken => _candidate.IsStudent == false;
    }
}
