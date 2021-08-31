using System;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Common.Exceptions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking
{
    // ToDo проверка сообщений об ошибках
    public class TestmakingTests: UnitTestBase
    {
        [Fact]
        public void When_TestCreated_Expect_TestHasVersionOne()
        {
            // arrange
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(title, topic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(0);
            // act
            Test t = new(title, topic, authorID, testCounterMock.Object);
            // assert
            Assert.Equal(1, t.Version);
        }

        [Fact]
        public void When_TestAlreadyExists_Expect_CreatingAnotherTest_IsNotPossible()
        {
            // arrange
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            // TBD какая версия теста должна передаваться?
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(title, topic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(1);

            // act
            Action act = () => { Test t = new(title, topic, authorID, testCounterMock.Object); };

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void ChangingTitleAndTopic_When_SimilarTestExists_IsNotPossible()
        {
            // arrange
            string intialTitle = "Политология как наука";
            string initialTopic = "Политология";
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(intialTitle, initialTopic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(0);

            string newTitle = "Политическая система";
            string newTopic = "Политология";
            // TBD какая версия теста должна передаваться?
            testCounterMock
                .Setup(c => c.GetQuantityOfTests(newTitle, newTopic, It.IsAny<int>(), authorID))
                .Returns(1);

            // act
            Test t = new(intialTitle, initialTopic, authorID, testCounterMock.Object);
            Action act = () => t.ChangeTitleAndTopic(newTitle, newTopic, testCounterMock.Object);

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void When_ChangingTimeLimit_ValueMustBeInRange()
        {
            // arrange
            Test t = MakeDefaultTest();

            // act
            TimeSpan oneSecond = new(0, 0, 1);
            TimeSpan tooBig = TimeLimitMustBeInRangeRule.MAX_LIMIT + oneSecond;
            TimeSpan tooSmall = TimeLimitMustBeInRangeRule.MIN_LIMIT - oneSecond;
            Action setTooBig = () => t.ChangeTimeLimit(tooBig);
            Action setTooSmall = () => t.ChangeTimeLimit(tooSmall);

            // assert
            Assert.Throws<BusinessException>(setTooBig);
            Assert.Throws<BusinessException>(setTooSmall);
        }

        [Fact]
        public void When_ChangingNumberOfAttempts_ValueMustBeInRange()
        {
            // arrange
            Test t = MakeDefaultTest();

            // act
            uint tooManyAttempts = AttemptsLimitForTestMustBeInRangeRule.MAX_ATTEMPTS_PER_TEST + 1;
            uint tooFewAttempts = AttemptsLimitForTestMustBeInRangeRule.MIN_ATTEMPTS_PER_TEST - 1;
            Action setTooManyAttempts = () => t.ChangeAttemptsLimit(tooManyAttempts);
            Action setTooFewAttempts = () => t.ChangeAttemptsLimit(tooFewAttempts);

            // assert
            Assert.Throws<BusinessException>(setTooManyAttempts);
            Assert.Throws<BusinessException>(setTooFewAttempts);
        }

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
            return defaultTest;
        }
        #endregion
    }
}
