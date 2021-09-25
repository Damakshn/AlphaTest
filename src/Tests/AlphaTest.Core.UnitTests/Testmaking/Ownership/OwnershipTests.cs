using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Ownership
{
    public class OwnershipTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(HelpersForUsers.NonTeacherRoles), MemberType = typeof(HelpersForUsers))]
        public void Non_teacher_user_cannot_be_set_as_new_test_author(UserRole role)
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData userTestData = new() { InitialRole = role };
            User newAuthor = HelpersForUsers.CreateUser(userTestData);
            AssertBrokenRule<OnlyTeacherCanBeSetAsNewAuthorOrContributorRule>(() =>
                test.SwitchAuthor(newAuthor)
            );
        }

        [Fact]
        public void Suspended_user_cannot_be_set_as_new_test_author()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData userTestData = new() { InitialRole = UserRole.TEACHER };
            User newAuthor = HelpersForUsers.CreateUser(userTestData);
            newAuthor.Suspend();
            AssertBrokenRule<SuspendedUserCannotBeSetAsNewAuthorOrContributorRule>(() =>
                test.SwitchAuthor(newAuthor)
            );
        }
    }
}
