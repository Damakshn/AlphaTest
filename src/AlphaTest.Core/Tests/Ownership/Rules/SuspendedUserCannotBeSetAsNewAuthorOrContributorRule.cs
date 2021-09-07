using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class SuspendedUserCannotBeSetAsNewAuthorOrContributorRule : IBusinessRule
    {
        private readonly User _user;

        public SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(User user)
        {
            _user = user;
        }
        public string Message => "Нельзя передать авторство заблокированному пользователю.";

        public bool IsBroken => _user.IsSuspended;
    }
}
