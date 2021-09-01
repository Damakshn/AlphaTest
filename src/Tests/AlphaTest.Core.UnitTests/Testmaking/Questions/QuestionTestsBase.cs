using System;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.TestingHelpers;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public abstract class QuestionTestsBase: UnitTestBase
    {
        protected static Test GetDefaultTest()
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

        protected static QuestionScore GetRandomScore()
        {
            Random random = new();
            return new QuestionScore(
                random.Next(
                    1,
                    100
                )
            );
        }

        protected class QuestionTestData
        {
            public QuestionTestData()
            {
                CounterMock.Setup(m => m.GetNumberOfQuestionsInTest(Test.ID)).Returns(0);
            }

            internal Mock<IQuestionCounter> CounterMock = new Mock<IQuestionCounter>();

            internal Test Test { get; set; } = GetDefaultTest();

            internal QuestionScore Score { get; set; } = GetRandomScore();

            internal QuestionText Text { get; set; } = new("Произвольный текст вопроса");
        }
    }
}
