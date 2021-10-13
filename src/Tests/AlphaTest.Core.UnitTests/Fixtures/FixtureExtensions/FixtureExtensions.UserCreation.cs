using AutoFixture;
using Moq;
using AlphaTest.Core.Users;
using System;

namespace AlphaTest.Core.UnitTests.Fixtures.FixtureExtensions
{
    internal static partial class FixtureExtensions
    {
        /*
        Методы расширения, создающие пользователей, используют Moq напрямую,
        кастомизации Autofixture на них никак не влияют.
        Это сделано из-за https://github.com/AutoFixture/AutoFixture/issues/1082#issuecomment-444630742
        Пока не придумал, как решить по-другому
        */
        public static IAlphaTestUser CreateStudent(this IFixture fixture)
        {
            var mockedStudent = fixture.CreateUserMock();
            mockedStudent.Setup(m => m.IsStudent).Returns(true);
            return mockedStudent.Object;
        }

        public static IAlphaTestUser CreateTeacher(this IFixture fixture)
        {
            var mockedStudent = fixture.CreateUserMock();
            mockedStudent.Setup(m => m.IsTeacher).Returns(true);
            return mockedStudent.Object;
        }

        public static IAlphaTestUser CreateAdmin(this IFixture fixture)
        {
            var mockedStudent = fixture.CreateUserMock();
            mockedStudent.Setup(m => m.IsAdmin).Returns(true);
            return mockedStudent.Object;
        }

        public static Mock<IAlphaTestUser> CreateUserMock(this IFixture fixture)
        {
            var mockedUser = new Mock<IAlphaTestUser>();
            mockedUser.Setup(u => u.ID).Returns(Guid.NewGuid());
            mockedUser.Setup(u => u.IsSuspended).Returns(false);
            return mockedUser;
        }
    }
}
