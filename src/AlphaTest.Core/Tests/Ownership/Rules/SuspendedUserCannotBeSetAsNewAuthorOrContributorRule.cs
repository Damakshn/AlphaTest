using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class SuspendedUserCannotBeSetAsNewAuthorOrContributorRule : IBusinessRule
    {
        private readonly IAlphaTestUser _user;

        public SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(IAlphaTestUser user)
        {
            _user = user;
        }
        public string Message => "Нельзя передать авторство заблокированному пользователю.";

        public bool IsBroken => _user.IsSuspended;
    }
}
