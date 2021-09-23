using System.Collections.Generic;
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

        public static IEnumerable<object[]> NonStudentRoles =>
            UserRole.All
            .Where(r => r != UserRole.STUDENT)
            .Select(r => new object[] { r })
            .ToList();
    }
}
