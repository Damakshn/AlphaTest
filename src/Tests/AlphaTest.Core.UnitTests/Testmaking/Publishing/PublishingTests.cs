using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.UnitTests.Testmaking.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Publishing.Rules;
using AlphaTest.Core.Tests.Questions;


namespace AlphaTest.Core.UnitTests.Testmaking.Publishing
{

    public class PublishingTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(NonDraftTestStatuses))]
        public void Non_draft_tests_cannot_be_proposed_for_publishing(TestStatus status)
        {
            Test test = HelpersForTests.GetDefaultTest();
            // статус устанавливаем через рефлексию, так как не важно, каким образом этот статус был достигнут
            // для установки обычным путём нужно много операций
            HelpersForTests.SetNewStatusForTest(test, status);
            AssertBrokenRule<OnlyDraftTestsCanBeProposedForPublishingRule>(() =>
                test.ProposeForPublishing(new List<Question>())
            );

        }

        [Fact]
        public void Test_without_questions_cannot_be_proposed_for_publishing()
        {
            Test test = HelpersForTests.GetDefaultTest();
            AssertBrokenRule<QuestionListMustNotBeEmptyBeforePublishingRule>(() =>
                test.ProposeForPublishing(new List<Question>())
            );
        }

        [Fact]
        public void Test_with_unachievable_score_cannot_be_proposed_for_publishing()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(1000);
            AssertBrokenRule<PassingScoreMustBeAchievableRule>(() =>
                test.ProposeForPublishing(new List<Question>() { question })
            );
        }

        [Fact]
        public void Draft_test_with_questions_and_realistic_score_can_be_proposed_for_publishing()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            Assert.Equal(test.ID, proposal.TestID);
            Assert.Equal(test.Status, TestStatus.WaitingForPublishing);
        }

        [Fact]
        public void Publishing_proposal_cannot_be_declined_without_remark()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            AssertBrokenRule<RemarkMustBeProvidedWhenProposalIsDeclinedRule>(() =>
                proposal.Decline("    ")
            );
        }

        [Fact]
        public void Publishing_proposal_can_be_declined_with_remark()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            proposal.Decline("Текст замечания");

            Assert.Equal(ProposalStatus.DECLINED, proposal.Status);
            // ToDo check domain event
        }

        [Fact]
        public void Test_must_be_in_waiting_mode_before_publishing()
        {
            Test test = HelpersForTests.GetDefaultTest();
            AssertBrokenRule<TestMustBeProposedForPublishingBeforeBeingPublishedRule>(() =>
                test.Publish(null)
            );
        }

        [Fact]
        public void Publishing_proposal_must_be_provided_for_publishing()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            AssertBrokenRule<ProposalMustBeProvidedForPublishingRule>(() =>
                test.Publish(null)
            );
        }

        [Theory]
        [MemberData(nameof(NonApprovedProposalStatuses))]
        public void Publishing_test_requires_approved_proposal(ProposalStatus status)
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            SetProposalStatus(proposal, status);

            AssertBrokenRule<PublishingOfTestRequiresApprovedProposalRule>(() =>
                test.Publish(proposal)
            );
        }

        [Fact]
        public void Test_can_be_published_with_approved_proposal()
        {
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new() { Test = test, Score = new(10) };
            SingleChoiceQuestion question = QuestionTestSamples.CreateSingleChoiceQuestion(data) as SingleChoiceQuestion;
            test.ChangePassingScore(10);
            PublishingProposal proposal = test.ProposeForPublishing(new List<Question>() { question });

            SetProposalStatus(proposal, ProposalStatus.APPROVED);
            test.Publish(proposal);

            Assert.Equal(TestStatus.Published, test.Status);
            // ToDo check domain event
        }

        public static IEnumerable<object[]> NonDraftTestStatuses() =>
            TestStatus
            .All
            .Where(s => s != TestStatus.Draft)
            .Select(s => new object[] { s })
            .ToList();

        public static IEnumerable<object[]> NonApprovedProposalStatuses() =>
            ProposalStatus
            .All
            .Where(s => s != ProposalStatus.APPROVED)
            .Select(s => new object[] { s })
            .ToList();

        private static void SetProposalStatus(PublishingProposal proposal, ProposalStatus status)
        {
            if (proposal is null)
                throw new ArgumentNullException(nameof(proposal));
            if (status is null)
                throw new ArgumentNullException(nameof(status));
            var property = proposal.GetType().GetProperty("Status", BindingFlags.Public | BindingFlags.Instance);
            if (property is null)
                throw new InvalidOperationException($"Свойство Status не найдено у типа {proposal.GetType()}.");
            property.SetValue(proposal, status);
        }
    }
}
