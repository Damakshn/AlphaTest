using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Profile.ChangePassword
{
    public class ChangePasswordUseCaseRequest : IUseCaseRequest
    {
        public ChangePasswordUseCaseRequest(Guid userID, string oldPassword, string newPassword, string newPasswordRepeat)
        {
            UserID = userID;
            OldPassword = oldPassword;
            NewPassword = newPassword;
            NewPasswordRepeat = newPasswordRepeat;
        }

        public Guid UserID { get; private set; }

        public string OldPassword { get; private set; }

        public string NewPassword { get; private set; }

        public string NewPasswordRepeat { get; private set; }
    }
}
