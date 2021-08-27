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
    public class QuestionWithNumericAnswerTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateQuestionWithNumericAnswer_WithTooLongOrToShortText_IsNotPossible(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            uint score = 1;
            var counterMock = new Mock<IQuestionCounter>();
            decimal rightAnswer = 42;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddQuestionWithNumericAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionTextLengthMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateQuestionWithNumericAnswer_WhenTextLengthWithinRange_IsOk(string questionText)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            uint score = 1;
            var counterMock = new Mock<IQuestionCounter>();
            decimal rightAnswer = 42;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            QuestionWithNumericAnswer question = test.AddQuestionWithNumericAnswer(questionText, score, rightAnswer, counterMock.Object);
            
            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(questionText, question.Text);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void CreateQuestionWithNumericAnswer_WhenScoreOutOfRange_IsNotPossible(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            string questionText = "Текст вопроса";
            decimal rightAnswer = 42;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddQuestionWithNumericAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionScoreMustBeInRangeRule>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(85)]
        [InlineData(100)]
        public void CreateQuestionWithNumericAnswer_WhenScoreWithinRange_IsOk(uint score)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            var counterMock = new Mock<IQuestionCounter>();
            string questionText = "Текст вопроса";
            decimal rightAnswer = 42;
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            QuestionWithNumericAnswer question = test.AddQuestionWithNumericAnswer(questionText, score, rightAnswer, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(score, question.Score);
        }
    }
}
