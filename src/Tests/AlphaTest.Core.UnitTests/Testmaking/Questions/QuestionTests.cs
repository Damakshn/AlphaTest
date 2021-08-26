using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Common.Exceptions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.TestingHelpers;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionOptionsNoneRight))]
        [MemberData(nameof(QuestionOptionsManyRight))]
        public void CreatingSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test test = MakeDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            uint score = QuestionScoreMustBeInRange.MIN_SCORE;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => test.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionOptionsNoneRight))]
        public void CreatingMultiChoiceQuestion_WithNoneRightOptions_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test test = MakeDefaultTest();
            string questionText = "This game has no name";
            uint score = QuestionScoreMustBeInRange.MIN_SCORE;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => test.AddMultiChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void AddingQuestionWithDetailedAnswer_WhenCheckingMethodIsAutomatic_IsNotPossible()
        {
            // arrange
            Test test = MakeDefaultTest();
            test.ChangeWorkCheckingMethod(WorkCheckingMethod.AUTOMATIC, new List<Question>());
            string questionText = "Расскажите, как вы провели лето";
            uint score = QuestionScoreMustBeInRange.MIN_SCORE;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => test.AddQuestionWithDetailedAnswer(questionText, score, questionCounterMock.Object);

            // assert
            Assert.Throws<BusinessException>(act);
        }

        #region Тестовые данные
        public static IEnumerable<object[]> QuestionOptionsNoneRight =>
            new List<object[]>
            {
                new object[]
                {
                    // нет правильных ответов
                    new List<QuestionOption>
                    {
                        new QuestionOption("Первый вариант", 1, false),
                        new QuestionOption("Второй вариант", 2, false),
                        new QuestionOption("Третий вариант", 3, false),
                    },
                }
            };

        public static IEnumerable<object[]> QuestionOptionsOneRight =>
            new List<object[]>
            {
                new object[]
                {
                    // один правильный ответ
                    new List<QuestionOption>
                    {
                        new QuestionOption("Первый вариант", 1, true),
                        new QuestionOption("Второй вариант", 2, false),
                        new QuestionOption("Третий вариант", 3, false),
                    },                    
                }
            };

        public static IEnumerable<object[]> QuestionOptionsManyRight =>
            new List<object[]>
            {
                new object[]
                {
                    // более одного правильного ответа
                    new List<QuestionOption>
                    {
                        new QuestionOption("Первый вариант", 1, true),
                        new QuestionOption("Второй вариант", 2, true),
                        new QuestionOption("Третий вариант", 3, false),
                    },
                }
            };


        #endregion

        #region Вспомогательные методы
        private Test MakeDefaultTest()
        {
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(title, topic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(0);
            Test defaultTest = new(title, topic, authorID, testCounterMock.Object);
            EntityIDSetter.SetIDTo(defaultTest, 1);
            return defaultTest;
        }
        #endregion
    }
}
