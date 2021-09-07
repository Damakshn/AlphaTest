using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class ProposalCanBeAssignedOnlyToAdminUsersRule : IBusinessRule
    {
        private readonly User _user;

        public ProposalCanBeAssignedOnlyToAdminUsersRule(User user)
        {
            _user = user;
        }

        public string Message => "Заявку может обработать только администратор.";

        public bool IsBroken => _user.IsInRole(UserRole.ADMIN) == false;
    }
}
