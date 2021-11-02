using AlphaTest.Core.Common;

namespace AlphaTest.Core.Users.Rules
{
    public class AdminUserCannotBeSuspendedRule : IBusinessRule
    {
        private readonly IAlphaTestUser _user;

        public AdminUserCannotBeSuspendedRule(IAlphaTestUser user)
        {
            _user = user;
        }

        public string Message => "Пользователь с правами администратора не может быть заблокирован.";

        public bool IsBroken => _user.IsAdmin;
    }
}
