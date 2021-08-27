using System;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithTextualAnswerTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateQuestionWithTextualAnswer_WithTooLongOrToShortText_IsNotPossible(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            uint score = 1;
            var counterMock = new Mock<IQuestionCounter>();
            string rightAnswer = "Правильный ответ";
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddQuestionWithTextualAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionTextLengthMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateQuestionWithTextualAnswer_WhenTextLengthWithinRange_IsOk(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            uint score = 1;
            var counterMock = new Mock<IQuestionCounter>();
            string rightAnswer = "Правильный ответ";
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            QuestionWithTextualAnswer question = test.AddQuestionWithTextualAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(questionText, question.Text);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void CreateQuestionWithTextualAnswer_WhenScoreOutOfRange_IsNotPossible(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            string questionText = "Текст вопроса";
            string rightAnswer = "Правильный ответ";
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddQuestionWithTextualAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionScoreMustBeInRangeRule>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(85)]
        [InlineData(100)]
        public void CreateQuestionWithTextualAnswer_WhenScoreWithinRange_IsOk(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            string questionText = "Текст вопроса";
            string rightAnswer = "Правильный ответ";
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            QuestionWithTextualAnswer question = test.AddQuestionWithTextualAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(score, question.Score);
        }
    }
}
