using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class ProposalCanBeAssignedOnlyToAdminUsersRule : IBusinessRule
    {
        private readonly IAlphaTestUser _user;

        public ProposalCanBeAssignedOnlyToAdminUsersRule(IAlphaTestUser user)
        {
            _user = user;
        }

        public string Message => "Заявку может обработать только администратор.";

        public bool IsBroken => _user.IsAdmin == false;
    }
}
