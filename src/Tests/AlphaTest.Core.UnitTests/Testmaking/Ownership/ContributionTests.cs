using System.Linq;
using Xunit;
using Moq;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Core.Tests.Ownership.Rules;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using System;

namespace AlphaTest.Core.UnitTests.Testmaking.Ownership
{
    public class ContributionTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(HelpersForUsers.NonTeacherRoles), MemberType = typeof(HelpersForUsers))]
        public void Non_teacher_user_cannot_be_set_as_contributor(UserRole role)
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = role };
            User contributor = HelpersForUsers.CreateUser(data);
            AssertBrokenRule<OnlyTeacherCanBeSetAsNewAuthorOrContributorRule>(() =>
                test.AddContributor(contributor)
            );
        }

        [Fact]
        public void Suspended_user_cannot_be_set_as_contributor()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);
            contributor.Suspend();
            AssertBrokenRule<SuspendedUserCannotBeSetAsNewAuthorOrContributorRule>(() =>
                test.AddContributor(contributor)
            );
        }

        [Fact]
        public void Active_teacher_user_can_be_set_as_contributor()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);
            
            test.AddContributor(contributor);

            Assert.Equal(1, test.Contributions.Count(c => c.TeacherID == contributor.ID));
        }

        [Fact]
        public void Teacher_can_be_added_to_contributors_only_once()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);

            test.AddContributor(contributor);

            AssertBrokenRule<TeacherCanBeAddedToContributorsOnlyOnceRule>(() =>
                test.AddContributor(contributor)    
            );
        }

        [Fact]
        public void Non_contributor_teacher_cannot_be_removed_from_contributors()
        {
            Test test = HelpersForTests.GetDefaultTest();

            AssertBrokenRule<NonContributorTeacherCannotBeRemovedFromContributorsRule>(() =>
                test.RemoveContributor(Guid.NewGuid())
            );
        }

        [Fact]
        public void Teacher_can_be_removed_from_contributors()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);
            test.AddContributor(contributor);

            Assert.Equal(1, test.Contributions.Count(c => c.TeacherID == contributor.ID));
            test.RemoveContributor(contributor.ID);
            Assert.Equal(0, test.Contributions.Count(c => c.TeacherID == contributor.ID));
        }
    }
}
