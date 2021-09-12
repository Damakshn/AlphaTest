using System.Collections.Generic;
using AlphaTest.TestingHelpers;
using AlphaTest.Core.Users;
using System.Linq;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForUsers
    {
        public static User CreateUser(UserTestData data)
        {
            User user = new User(
                data.FirstName,
                data.LastName,
                data.MiddleName,
                data.Email,
                data.InitialRole,
                data.TemporaryPassword);
            EntityIDSetter.SetIDTo(user, data.ID != default ? data.ID : 1);
            return user;
        }

        public static IEnumerable<object[]> NonTeacherRoles =>
            UserRole.All
            .Where(r => r != UserRole.TEACHER)
            .Select(r => new object[] { r })
            .ToList();
            

        public static IEnumerable<object[]> NonAdminRoles =>
            UserRole.All
            .Where(r => r != UserRole.ADMIN)
            .Select(r => new object[] { r })
            .ToList();
    }
}
