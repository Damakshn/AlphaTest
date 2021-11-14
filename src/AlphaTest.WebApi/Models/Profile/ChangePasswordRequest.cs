namespace AlphaTest.WebApi.Models.Profile
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordRepeat { get; set; }

    }
}
