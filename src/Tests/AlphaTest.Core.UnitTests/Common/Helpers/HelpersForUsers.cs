using System.Collections.Generic;
using AlphaTest.TestingHelpers;
using AlphaTest.Core.Users;

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
            new List<object[]> 
            { 
                new object[]{UserRole.ADMIN},
                new object[]{UserRole.STUDENT},
            };

        public static IEnumerable<object[]> NonAdminRoles =>
            new List<object[]>
            {
                new object[]{UserRole.TEACHER},
                new object[]{UserRole.STUDENT},
            };
    }
}
