using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;


namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class SingleChoiceQuestionTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestData.Options_NoneOrManyRight), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Кто проживает на дне океана?");
            QuestionScore score = GetRandomScore();
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule>(act);
        }

        [Fact]
        public void CreateSingleChoiceQuestion_WithExactlyOneRightOption_IsOk()
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Кто проживает на дне океана?");
            QuestionScore score = GetRandomScore();
            var questionCounterMock = new Mock<IQuestionCounter>();
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(1, question.Options.Count(o => o.IsRight));
            Assert.Equal(score, question.Score);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Кто проживает на дне океана?");
            QuestionScore score = GetRandomScore();
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsNotBroken_IsOk(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Кто проживает на дне океана?");
            QuestionScore score = GetRandomScore();
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(question.Options.Count, options.Count);
            Assert.Equal(score, question.Score);
        }

        [Fact]
        public void WhenCreateSingleChoiceQuestion_WithUnifiedScore_ScoreFromUserIsIgnored()
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            QuestionScore unifiedScore = new(10);
            t.ChangeScoreDistributionMethod(ScoreDistributionMethod.UNIFIED);
            t.ChangeScorePerQuestion(unifiedScore);

            QuestionText questionText = new("Кто проживает на дне океана?");
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            QuestionScore score = new(5);

            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(t.ID)).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(unifiedScore, question.Score);
            Assert.NotEqual(score, question.Score);
        }     

        private QuestionScore GetRandomScore()
        {
            Random random = new();
            return new QuestionScore(
                random.Next(
                    QuestionScoreMustBeInRangeRule.MIN_SCORE,
                    QuestionScoreMustBeInRangeRule.MAX_SCORE
                )
            );
        }
    }
}
