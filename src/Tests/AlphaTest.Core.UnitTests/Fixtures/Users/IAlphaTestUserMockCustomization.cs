using AlphaTest.Core.Users;
using AutoFixture;
using Moq;
using System;

namespace AlphaTest.Core.UnitTests.Fixtures.Users
{
    internal class IAlphaTestUserMockCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {   
            fixture.Customize<Mock<IAlphaTestUser>>(c => 
                c.FromFactory(() =>
                    {
                        var mockedUser = new Mock<IAlphaTestUser>();
                        mockedUser.Setup(u => u.IsPasswordChanged).Returns(true);
                        mockedUser.Setup(u => u.ID).Returns(Guid.NewGuid());
                        mockedUser.Setup(u => u.IsSuspended).Returns(false);
                        mockedUser.Setup(u => u.IsAdmin).Returns(false);
                        mockedUser.Setup(u => u.IsTeacher).Returns(false);
                        mockedUser.Setup(u => u.IsStudent).Returns(false);
                        return mockedUser;
                    }));
        }
    }
}
