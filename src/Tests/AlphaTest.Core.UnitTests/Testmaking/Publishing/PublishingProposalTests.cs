using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Publishing.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Testmaking.Questions;
using System.Collections.Generic;
using AlphaTest.Core.UnitTests.Fixtures;
using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.FixtureExtensions;

namespace AlphaTest.Core.UnitTests.Testmaking.Publishing
{
    public class PublishingProposalTests: UnitTestBase
    {
        [Theory, TestmakingTestsData]
        public void Publishing_proposal_cannot_be_assigned_to_non_admin_user(Test test, IFixture fixture)
        {
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal sut = test.ProposeForPublishing(new List<Question>() { question });

            var mockedUser = fixture.CreateUserMock();
            mockedUser.Setup(u => u.IsAdmin).Returns(false);
            IAlphaTestUser assignee = mockedUser.Object;

            AssertBrokenRule<ProposalCanBeAssignedOnlyToAdminUsersRule>(() =>
                sut.AssignTo(assignee)
            );
        }
    }
}
