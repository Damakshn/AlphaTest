using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    public class SuspendedUsersCannotBeIncludedIntoGroupRule : IBusinessRule
    {
        private readonly IAlphaTestUser _candidate;

        public SuspendedUsersCannotBeIncludedIntoGroupRule(IAlphaTestUser candidate)
        {
            _candidate = candidate;
        }

        public string Message => "Заблокированных пользователей нельзя добавлять в группы.";

        public bool IsBroken => _candidate.IsSuspended;
    }
}
