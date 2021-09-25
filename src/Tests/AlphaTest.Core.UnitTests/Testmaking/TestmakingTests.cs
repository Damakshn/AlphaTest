using System;
using System.Linq;
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
using AlphaTest.Core.Users;

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
            Guid authorID = Guid.NewGuid();
            // act
            Test t = new(title, topic, authorID, false);
            // assert
            Assert.Equal(1, t.Version);
        }

        [Fact]
        public void When_TestAlreadyExists_Expect_CreatingAnotherTest_IsNotPossible()
        {
            // arrange
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            Guid authorID = Guid.NewGuid();
            
            // act
            Action act = () => { Test t = new(title, topic, authorID, true); };

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void ChangingTitleAndTopic_When_SimilarTestExists_IsNotPossible()
        {
            // arrange
            string intialTitle = "Политология как наука";
            string initialTopic = "Политология";
            Guid authorID = Guid.NewGuid();

            string newTitle = "Политическая система";
            string newTopic = "Политология";

            // act
            Test t = new(intialTitle, initialTopic, authorID, false);
            Action act = () => t.ChangeTitleAndTopic(newTitle, newTopic, true);

            // assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void When_ChangingTimeLimit_ValueMustBeInRange()
        {
            // arrange
            Test t = HelpersForTests.GetDefaultTest();

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
            Test t = HelpersForTests.GetDefaultTest();

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
            Test t = HelpersForTests.GetDefaultTest();
            AssertBrokenRule<ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule>(() =>
                t.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, null)
            );
        }

        [Fact]
        public void SetUnifiedScoreDistribution_WithScore_IsOk()
        {
            Test t = HelpersForTests.GetDefaultTest();
            QuestionScore unifiedScore = new(50);
            t.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);
            Assert.Equal(unifiedScore, t.ScorePerQuestion);
        }

        [Theory]
        [MemberData(nameof(AllSettingsEditingActions))]
        public void EditAnySettings_WhenTestIsPublished_IsNotPossible(Action<Test> editingDelegate)
        {
            Test test = HelpersForTests.GetDefaultTest();
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


        // ToDo NAMING
        [Fact]
        public void Replicated_test_has_same_checking_and_procedure_settings_as_source_test() 
        {
            Test test = HelpersForTests.GetDefaultTest();

            test.ChangeWorkCheckingMethod(WorkCheckingMethod.AUTOMATIC, new List<Question>());
            test.ChangeCheckingPolicy(CheckingPolicy.HARD);
            test.ChangeNavigationMode(NavigationMode.FREE);
            test.ChangePassingScore(250);
            test.ChangeTimeLimit(new TimeSpan(2, 0, 0));
            test.ChangeTitleAndTopic("Новое название", "Новая тема", false);
            test.ChangeAttemptsLimit(3);

            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test replica = test.Replicate();

            Assert.Equal(test.WorkCheckingMethod, replica.WorkCheckingMethod);
            Assert.Equal(test.CheckingPolicy, replica.CheckingPolicy);
            Assert.Equal(test.NavigationMode, replica.NavigationMode);
            Assert.Equal(test.PassingScore, replica.PassingScore);
            Assert.Equal(test.TimeLimit, replica.TimeLimit);
            Assert.Equal(test.Title, replica.Title);
            Assert.Equal(test.Topic, replica.Topic);
            Assert.Equal(test.AttemptsLimit, replica.AttemptsLimit);
        }

        [Fact]
        public void Replicated_test_has_same_set_of_contributors_as_source_test()
        {
            Test test = HelpersForTests.GetDefaultTest();

            UserTestData udata1 = new()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MiddleName = "Иванович",
                InitialRole = UserRole.TEACHER
            };
            UserTestData udata2 = new()
            {
                FirstName = "Смирнова",
                LastName = "Елена",
                MiddleName = "Сергеевна",
                InitialRole = UserRole.TEACHER
            };
            User user1 = HelpersForUsers.CreateUser(udata1);
            User user2 = HelpersForUsers.CreateUser(udata2);
            test.AddContributor(user1);
            test.AddContributor(user2);

            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test replica = test.Replicate();

            Assert.Equal(1, replica.Contributions.Count(c => c.TeacherID == user1.ID));
            Assert.Equal(1, replica.Contributions.Count(c => c.TeacherID == user2.ID));
        }

        [Fact]
        public void Replicated_test_is_always_draft()
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test replica = test.Replicate();
            Assert.Equal(TestStatus.Draft, replica.Status);
        }

        [Fact]
        public void Replicated_test_version_is_higher_than_source_test_version_for_one()
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test replica = test.Replicate();
            Assert.Equal(test.Version + 1, replica.Version);
        }

        [Fact]
        public void Only_published_test_can_be_replicated_for_new_version()
        {
            Test test = HelpersForTests.GetDefaultTest();
            AssertBrokenRule<OnlyPublishedTestsCanBeReplicatedRule>(() =>
                test.Replicate()
            );
        }


        #region Тестовые данные

        public static IEnumerable<object[]> AllSettingsEditingActions =>
            new List<object[]> {
                new object[]{ChangeAttemptsLimit},
                new object[]{ChangeTimeLimit},
                new object[]{ChangeRevokePolicy},
                new object[]{ChangeNavigationMode},
                new object[]{ChangeCheckingPolicy},
                new object[]{ChangeWorkCheckingMethod},
                new object[]{ChangePassingScore},
                new object[]{ConfigureScoreDistribution},
                new object[]{ChangeTitleAndTopic}
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
        public static readonly Action<Test> ChangeTitleAndTopic = test => test.ChangeTitleAndTopic("НовоеНазвание", "НоваяТема", false);
        #endregion

        #endregion
    }
}
