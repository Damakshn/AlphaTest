using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;


namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class MultiChoiceQuestionTests: UnitTestBase
    {
        [Fact]
        public void CreateMultiChoiceQuestion_WithoutRightOptions_IsNotPossible()
        {
            // arrange
            // ToDo плюс-минус один и тот же набор данных для вопроса, надо придумать, как убрать это во имя DRY
            Test test = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Что говорить, когда нечего говорить?");
            QuestionScore score = new(1);
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsNoneRight;

            // act
            Action act = () => test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            AssertBrokenRule<AtLeastOneQuestionOptionMustBeRightRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_OneOrManyRight), MemberType = typeof(QuestionTestData))]
        public void CreateMultiChoiceQuestion_WithOneOrManyRightOptions_IsOk(List<QuestionOption> options)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Что говорить, когда нечего говорить?");
            QuestionScore score = new(1);
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            //act
            MultiChoiceQuestion question = test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(score, question.Score);
            Assert.Equal(options.Count, question.Options.Count);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateMultiChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Что говорить, когда нечего говорить?");
            QuestionScore score = new(1);
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateMultiChoiceQuestion_WithOptionsNumberWithinRange_IsOk(List<QuestionOption> options)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            QuestionText questionText = new("Что говорить, когда нечего говорить?");
            QuestionScore score = new(1);
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            MultiChoiceQuestion question = test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(options.Count, question.Options.Count);
        }
    }
}
