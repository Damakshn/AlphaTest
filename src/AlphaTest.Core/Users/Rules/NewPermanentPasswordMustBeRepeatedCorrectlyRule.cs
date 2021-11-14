using AlphaTest.Core.Common;

namespace AlphaTest.Core.Users.Rules
{
    public class NewPermanentPasswordMustBeRepeatedCorrectlyRule : IBusinessRule
    {
        private readonly string _newPassword;
        private readonly string _newPasswordRepeat;

        public NewPermanentPasswordMustBeRepeatedCorrectlyRule(string newPassword, string newPasswordRepeat)
        {
            _newPassword = newPassword;
            _newPasswordRepeat = newPasswordRepeat;
        }

        public string Message => "Пароль повторён неправильно, попробуйте ещё раз.";

        public bool IsBroken => _newPassword != _newPasswordRepeat;
    }
}
