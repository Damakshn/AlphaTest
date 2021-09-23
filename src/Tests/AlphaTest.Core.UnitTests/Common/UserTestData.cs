using AlphaTest.Core.Users;

namespace AlphaTest.Core.UnitTests.Common
{
    public class UserTestData
    {
        public UserTestData(){ }

        public string FirstName { get; set; } = "Иванов";

        public string LastName { get; set; } = "Иван";

        public string MiddleName { get; set; } = "Иванович";

        public string Email { get; set; } = "overlord2000@mail.ru";

        public UserRole InitialRole { get; set; } = UserRole.STUDENT;

        public string TemporaryPassword { get; private set; } = "HeR3G0esThEpaSsWorD&";
    }
}
