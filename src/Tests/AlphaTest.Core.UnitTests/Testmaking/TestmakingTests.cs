using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Common.Exceptions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Testmaking.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;

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

        [Fact]
        public void SetUnifiedScoreDistribution_WithoutScore_IsNotPossible()
        {
            Test t = MakeDefaultTest();
            AssertBrokenRule<ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule>(() =>
                t.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, null)
            );
        }

        [Fact]
        public void SetUnifiedScoreDistribution_WithScore_IsOk()
        {
            Test t = MakeDefaultTest();
            QuestionScore unifiedScore = new(50);
            t.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);
            Assert.Equal(unifiedScore, t.ScorePerQuestion);
        }

        [Theory]
        [MemberData(nameof(AllSettingsEditingActions))]
        public void EditAnySettings_WhenTestIsPublished_IsNotPossible(Action<Test> editingDelegate)
        {
            Test test = MakeDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            AssertBrokenRule<NonDraftTestCannotBeEditedRule>(() => editingDelegate(test));
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void AddAnyQuestion_WhenTestIsPublished_IsNotPossible(Func<QuestionTestData, Question> addQuestionDelegate)
        {
            // ToDo выглядит плохо, надо будет исправить
            // MAYBE перенести в тесты для вопросов
            QuestionTestData data = new();
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Published);
            AssertBrokenRule<NonDraftTestCannotBeEditedRule>(() => addQuestionDelegate(data));
        }
        
        
        #region Вспомогательные методы
        private static Test MakeDefaultTest()
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

        #region Тестовые данные
        // ToDo не хватает редактирования темы и названия - туда нужно передавать Counter
        public static IEnumerable<object[]> AllSettingsEditingActions =>
            new List<object[]> {
                new object[]{ChangeAttemptsLimit},
                new object[]{ChangeTimeLimit},
                new object[]{ChangeRevokePolicy},
                new object[]{ChangeNavigationMode},
                new object[]{ChangeCheckingPolicy},
                new object[]{ChangeWorkCheckingMethod},
                new object[]{ChangePassingScore},
                new object[]{ConfigureScoreDistribution}
            };

        #region Делегаты с редактированием настроек
        public static readonly Action<Test> ChangeAttemptsLimit = test => test.ChangeAttemptsLimit(2);
        public static readonly Action<Test> ChangeTimeLimit = test => test.ChangeTimeLimit(TimeSpan.FromHours(2));
        public static readonly Action<Test> ChangeRevokePolicy = test => test.ChangeRevokePolicy(new RevokePolicy(true, 2));
        public static readonly Action<Test> ChangeNavigationMode = test => test.ChangeNavigationMode(NavigationMode.FREE);
        public static readonly Action<Test> ChangeCheckingPolicy = test => test.ChangeCheckingPolicy(CheckingPolicy.SOFT);
        public static readonly Action<Test> ChangeWorkCheckingMethod = test => test.ChangeWorkCheckingMethod(WorkCheckingMethod.MIXED, new List<Question>());
        public static readonly Action<Test> ChangePassingScore = test => test.ChangePassingScore(200);
        public static readonly Action<Test> ConfigureScoreDistribution = test => test.ConfigureScoreDistribution(ScoreDistributionMethod.MANUAL);
        #endregion

        #endregion
    }
}
