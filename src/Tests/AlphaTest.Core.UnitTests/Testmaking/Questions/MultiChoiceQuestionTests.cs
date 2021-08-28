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
            string questionText = "Что говорить, когда нечего говорить?";
            uint score = 1;
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
            string questionText = "Что говорить, когда нечего говорить?";
            uint score = 1;
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
            string questionText = "Что говорить, когда нечего говорить?";
            uint score = 1;
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
            string questionText = "Что говорить, когда нечего говорить?";
            uint score = 1;
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            MultiChoiceQuestion question = test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(options.Count, question.Options.Count);
        }

        // MAYBE сделать специальный тип QuestionScore и тестировать его валидацию?
        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void CreateMultiChoiceQuestion_WhenScoreOutOfRange_IsNotPossible(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            string questionText = "Что говорить, когда нечего говорить?";
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsManyRight;

            // act
            Action act = () => test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionScoreMustBeInRangeRule>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(85)]
        [InlineData(100)]
        public void CreateMultiChoiceQuestion_WhenScoreWithinRange_IsOk(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            string questionText = "Что говорить, когда нечего говорить?";
            var counterMock = new Mock<IQuestionCounter>();
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsManyRight;

            // act
            MultiChoiceQuestion question = test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(score, question.Score);
        }

        // MAYBE сделать специальный тип QuestionText и тестировать его валидацию?
        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateMultiChoiceQuestion_WithTooLongOrToShortText_IsNotPossible(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            uint score = 1;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsManyRight;

            // act
            Action act = () => test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionTextLengthMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateMultiChoiceQuestion_WhenTextLengthWithinRange_IsOk(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            uint score = 1;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsManyRight;

            // act
            MultiChoiceQuestion question =  test.AddMultiChoiceQuestion(questionText, score, options, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(questionText, question.Text);
        }
    }
}
