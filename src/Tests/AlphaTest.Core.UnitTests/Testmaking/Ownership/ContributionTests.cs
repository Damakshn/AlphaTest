using System.Linq;
using Xunit;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership.Rules;
using AlphaTest.Core.UnitTests.Common;
using System;
using AlphaTest.Core.UnitTests.Fixtures;
using AlphaTest.Core.UnitTests.Fixtures.FixtureExtensions;
using AutoFixture;


namespace AlphaTest.Core.UnitTests.Testmaking.Ownership
{
    public class ContributionTests: UnitTestBase
    {
        [Theory, TestmakingTestsData]
        public void Non_teacher_user_cannot_be_set_as_contributor(Test sut, IFixture fixture)
        {
            var mockedUser = fixture.CreateUserMock();
            mockedUser.Setup(u => u.IsTeacher).Returns(false);
            var contributor = mockedUser.Object;
            AssertBrokenRule<OnlyTeacherCanBeSetAsNewAuthorOrContributorRule>(() =>
                sut.AddContributor(contributor)
            );
        }

        [Theory, TestmakingTestsData]
        public void Suspended_user_cannot_be_set_as_contributor(Test sut, IFixture fixture)
        {
            var mockedUser = fixture.CreateUserMock();
            mockedUser.Setup(u => u.IsTeacher).Returns(true);
            mockedUser.Setup(u => u.IsSuspended).Returns(true);
            AssertBrokenRule<SuspendedUserCannotBeSetAsNewAuthorOrContributorRule>(() =>
                sut.AddContributor(mockedUser.Object)
            );
        }

        [Theory, TestmakingTestsData]
        public void Active_teacher_user_can_be_set_as_contributor(Test sut, IFixture fixture)
        {   
            var contributor = fixture.CreateTeacher();
            
            sut.AddContributor(contributor);

            Assert.Equal(1, sut.Contributions.Count(c => c.TeacherID == contributor.Id));
        }

        [Theory, TestmakingTestsData]
        public void Teacher_can_be_added_to_contributors_only_once(Test sut, IFixture fixture)
        {   
            var contributor = fixture.CreateTeacher();

            sut.AddContributor(contributor);

            AssertBrokenRule<TeacherCanBeAddedToContributorsOnlyOnceRule>(() =>
                sut.AddContributor(contributor)    
            );
        }

        [Theory, TestmakingTestsData]
        public void Non_contributor_teacher_cannot_be_removed_from_contributors(Test sut)
        {
            AssertBrokenRule<NonContributorTeacherCannotBeRemovedFromContributorsRule>(() =>
                sut.RemoveContributor(Guid.NewGuid())
            );
        }

        [Theory, TestmakingTestsData]
        public void Teacher_can_be_removed_from_contributors(Test sut, IFixture fixture)
        {   
            var contributor = fixture.CreateTeacher();
            sut.AddContributor(contributor);

            Assert.Equal(1, sut.Contributions.Count(c => c.TeacherID == contributor.Id));
            sut.RemoveContributor(contributor.Id);
            Assert.Equal(0, sut.Contributions.Count(c => c.TeacherID == contributor.Id));
        }
    }
}
