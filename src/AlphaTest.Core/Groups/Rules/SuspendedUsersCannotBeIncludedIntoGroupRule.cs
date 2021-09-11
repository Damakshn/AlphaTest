using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    public class SuspendedUsersCannotBeIncludedIntoGroupRule : IBusinessRule
    {
        private readonly User _candidate;

        public SuspendedUsersCannotBeIncludedIntoGroupRule(User candidate)
        {
            _candidate = candidate;
        }

        public string Message => "Заблокированных пользователей нельзя добавлять в группы.";

        public bool IsBroken => _candidate.IsSuspended;
    }
}
