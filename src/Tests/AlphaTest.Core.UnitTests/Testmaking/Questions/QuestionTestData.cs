using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using Moq;
using System;
using System.Collections.Generic;
using AlphaTest.TestingHelpers;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTestData
    {
        public QuestionTestData()
        {
            CounterMock.Setup(m => m.GetNumberOfQuestionsInTest(Test.ID)).Returns(0);
        }

        internal Mock<IQuestionCounter> CounterMock = new Mock<IQuestionCounter>();

        internal Test Test { get; set; } = GetDefaultTest();

        internal QuestionScore Score { get; set; } = GetRandomScore();

        internal QuestionText Text { get; set; } = new("Произвольный текст вопроса");

        internal string TextualAnswer { get; set; } = "Правильный ответ";

        internal decimal NumericAnswer { get; set; } = 1789;

        internal List<QuestionOption> Options { get; set; } = new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false),
            };

        public static Test GetDefaultTest()
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

        public static QuestionScore GetRandomScore()
        {
            Random random = new();
            return new QuestionScore(
                random.Next(
                    1,
                    100
                )
            );
        }
    }
}
