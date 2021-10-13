using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Testmaking.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;
using AlphaTest.Core.UnitTests.Fixtures;
using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.FixtureExtensions;

namespace AlphaTest.Core.UnitTests.Testmaking
{
    public class TestmakingTests: UnitTestBase
    {
        [Theory, TestmakingTestsData]
        public void New_test_has_version_one(string title, string topic, Guid authorID)
        {
            Test sut = new(title, topic, authorID, false);

            Assert.Equal(1, sut.Version);
        }

        [Theory, TestmakingTestsData]
        public void Test_cannot_be_created_if_such_test_already_exists(string title, string topic, Guid authorID)
        {   
            AssertBrokenRule<TestMustBeUniqueRule>(() => { Test sut = new(title, topic, authorID, true); });
        }

        [Theory, TestmakingTestsData]
        public void Title_and_topic_cannot_be_changed_if_such_test_already_exists(Test sut, string newTitle, string newTopic)
        {
            AssertBrokenRule<TestMustBeUniqueRule>(() => sut.ChangeTitleAndTopic(newTitle, newTopic, true));
        }

        [Theory, TestmakingTestsData]
        public void Time_limit_for_test_must_be_in_range(Test sut)
        {   
            TimeSpan oneSecond = new(0, 0, 1);
            TimeSpan tooBig = TimeLimitMustBeInRangeRule.MAX_LIMIT + oneSecond;
            TimeSpan tooSmall = TimeLimitMustBeInRangeRule.MIN_LIMIT - oneSecond;
            void setTooBig() => sut.ChangeTimeLimit(tooBig);
            void setTooSmall() => sut.ChangeTimeLimit(tooSmall);

            AssertBrokenRule<TimeLimitMustBeInRangeRule>(setTooBig);
            AssertBrokenRule<TimeLimitMustBeInRangeRule>(setTooSmall);
        }

        [Theory, TestmakingTestsData]
        public void Number_of_attempts_for_passing_test_must_be_in_range(Test sut)
        {
            uint tooManyAttempts = AttemptsLimitForTestMustBeInRangeRule.MAX_ATTEMPTS_PER_TEST + 1;
            uint tooFewAttempts = AttemptsLimitForTestMustBeInRangeRule.MIN_ATTEMPTS_PER_TEST - 1;
            void setTooManyAttempts() => sut.ChangeAttemptsLimit(tooManyAttempts);
            void setTooFewAttempts() => sut.ChangeAttemptsLimit(tooFewAttempts);

            AssertBrokenRule<AttemptsLimitForTestMustBeInRangeRule>(setTooManyAttempts);
            AssertBrokenRule<AttemptsLimitForTestMustBeInRangeRule>(setTooFewAttempts);
        }

        [Theory, TestmakingTestsData]
        public void Unified_score_value_must_be_provided(Test sut)
        {   
            AssertBrokenRule<ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule>(() =>
                sut.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, null)
            );
        }

        [Theory, TestmakingTestsData]
        public void Unified_score_can_be_defined_in_test_settings(Test sut)
        {
            QuestionScore unifiedScore = new(50);

            sut.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);

            Assert.Equal(unifiedScore, sut.ScorePerQuestion);
        }

        // ToDo autofixture
        [Theory]
        [MemberData(nameof(AllSettingsEditingActions))]
        public void Test_cannot_be_modified_after_publishing(Action<Test> editingDelegate)
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            AssertBrokenRule<NonDraftTestCannotBeEditedRule>(() => editingDelegate(test));
        }

        // ToDo autofixture
        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void Questions_cannot_be_added_when_test_is_published(Func<QuestionTestData, Question> addQuestionDelegate)
        {
            // ToDo выглядит плохо, надо будет исправить
            // MAYBE перенести в тесты для вопросов
            QuestionTestData data = new();
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Published);
            AssertBrokenRule<NonDraftTestCannotBeEditedRule>(() => addQuestionDelegate(data));
        }

        [Theory, TestmakingTestsData]
        public void Replicated_test_has_same_checking_and_procedure_settings_as_source_test(Test sut) 
        {
            sut.ChangeWorkCheckingMethod(WorkCheckingMethod.AUTOMATIC, new List<Question>());
            sut.ChangeCheckingPolicy(CheckingPolicy.HARD);
            sut.ChangeNavigationMode(NavigationMode.FREE);
            sut.ChangePassingScore(250);
            sut.ChangeTimeLimit(new TimeSpan(2, 0, 0));
            sut.ChangeTitleAndTopic("Новое название", "Новая тема", false);
            sut.ChangeAttemptsLimit(3);

            HelpersForTests.SetNewStatusForTest(sut, TestStatus.Published);
            Test replica = sut.Replicate();

            Assert.Equal(sut.WorkCheckingMethod, replica.WorkCheckingMethod);
            Assert.Equal(sut.CheckingPolicy, replica.CheckingPolicy);
            Assert.Equal(sut.NavigationMode, replica.NavigationMode);
            Assert.Equal(sut.PassingScore, replica.PassingScore);
            Assert.Equal(sut.TimeLimit, replica.TimeLimit);
            Assert.Equal(sut.Title, replica.Title);
            Assert.Equal(sut.Topic, replica.Topic);
            Assert.Equal(sut.AttemptsLimit, replica.AttemptsLimit);
        }

        [Theory, TestmakingTestsData]
        public void Replicated_test_has_same_set_of_contributors_as_source_test(Test sut, IFixture fixture)
        {   
            IAlphaTestUser user1 = fixture.CreateTeacher();
            IAlphaTestUser user2 = fixture.CreateTeacher();
            sut.AddContributor(user1);
            sut.AddContributor(user2);

            HelpersForTests.SetNewStatusForTest(sut, TestStatus.Published);
            Test replica = sut.Replicate();

            Assert.Equal(1, replica.Contributions.Count(c => c.TeacherID == user1.ID));
            Assert.Equal(1, replica.Contributions.Count(c => c.TeacherID == user2.ID));
        }

        [Theory, TestmakingTestsData]
        public void Replicated_test_is_always_draft(Test sut)
        {
            HelpersForTests.SetNewStatusForTest(sut, TestStatus.Published);
            Test replica = sut.Replicate();
            Assert.Equal(TestStatus.Draft, replica.Status);
        }

        [Theory, TestmakingTestsData]
        public void Replicated_test_version_is_higher_than_source_test_version_for_one(Test sut)
        {
            HelpersForTests.SetNewStatusForTest(sut, TestStatus.Published);
            Test replica = sut.Replicate();
            Assert.Equal(sut.Version + 1, replica.Version);
        }

        [Theory, TestmakingTestsData]
        public void Only_published_test_can_be_replicated_for_new_version(Test sut)
        {
            AssertBrokenRule<OnlyPublishedTestsCanBeReplicatedRule>(() =>
                sut.Replicate()
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
