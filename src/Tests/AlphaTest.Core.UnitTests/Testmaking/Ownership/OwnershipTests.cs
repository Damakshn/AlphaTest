using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership.Rules;
using AlphaTest.Core.UnitTests.Fixtures;
using Moq;

namespace AlphaTest.Core.UnitTests.Testmaking.Ownership
{
    public class OwnershipTests: UnitTestBase
    {
        [Theory]
        [TestmakingTestsData]
        public void Non_teacher_user_cannot_be_set_as_new_test_author(Test test, Mock<IAlphaTestUser> mockedUser)
        {
            mockedUser.Setup(u => u.IsTeacher).Returns(false);
            AssertBrokenRule<OnlyTeacherCanBeSetAsNewAuthorOrContributorRule>(() =>
                test.SwitchAuthor(mockedUser.Object)
            );
        }

        [Theory]
        [TestmakingTestsData]
        public void Suspended_user_cannot_be_set_as_new_test_author(Test test, Mock<IAlphaTestUser> mockedUser)
        {
            mockedUser.Setup(u => u.IsTeacher).Returns(true);
            mockedUser.Setup(u => u.IsSuspended).Returns(true);
            AssertBrokenRule<SuspendedUserCannotBeSetAsNewAuthorOrContributorRule>(() =>
                test.SwitchAuthor(mockedUser.Object)
            );
        }
    }
}
