using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Publishing.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Testmaking.Questions;
using System.Collections.Generic;

namespace AlphaTest.Core.UnitTests.Testmaking.Publishing
{
    public class PublishingProposalTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(HelpersForUsers.NonAdminRoles), MemberType = typeof(HelpersForUsers))]
        public void Publishing_proposal_cannot_be_assigned_to_non_admin_user(UserRole role)
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            UserTestData userData = new() { ID = 5, InitialRole = role };
            User assignee = HelpersForUsers.CreateUser(userData);

            AssertBrokenRule<ProposalCanBeAssignedOnlyToAdminUsersRule>(() =>
                proposal.AssignTo(assignee)
            );
        }
    }
}
