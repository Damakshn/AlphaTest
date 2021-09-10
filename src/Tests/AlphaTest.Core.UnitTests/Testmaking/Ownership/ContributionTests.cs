﻿using Xunit;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Core.Tests.Ownership.Rules;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;

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
                new Contribution(test, contributor)
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
                new Contribution(test, contributor)
            );
        }

        [Fact]
        public void Active_teacher_user_can_be_set_as_contributor()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);

            Contribution contribution = new(test, contributor);

            Assert.Equal(test.ID, contribution.TestID);
            Assert.Equal(contributor.ID, contribution.TeacherID);
        }

        [Fact]
        public void Replicated_contribution_is_bound_to_different_test()
        {
            Test test = HelpersForTests.GetDefaultTest();
            UserTestData data = new() { ID = 5, InitialRole = UserRole.TEACHER };
            User contributor = HelpersForUsers.CreateUser(data);
            Contribution source = new(test, contributor);

            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test newEdition = test.Replicate();
            Contribution replica = source.ReplicateForNewEdition(newEdition);

            Assert.NotEqual(test.ID, replica.TestID);
            Assert.Equal(newEdition.ID, replica.TestID);

        }
    }
}
