namespace AlphaTest.WebApi.Models.Admin.UserManagement
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string TemporaryPassword { get; set; }

        public string InitialRole { get; set; }
    }
}
