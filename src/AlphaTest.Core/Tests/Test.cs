using System;
using System.Collections.Generic;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.Tests
{
    public class Test: Entity
    {
        public static readonly int INITIAL_VERSION = 1;

        #region Основные атрибуты
        public int ID { get; private set; }

        public string Title { get; private set; }

        public string Topic { get; private set; }

        // ToDo NewVersion, Replicate
        public int Version { get; private set; }

        // ToDo SwitchAuthor
        public int AuthorID { get; private set; }

        // ToDo Contributors

        // ToDo Publish
        // ToDo Cannot edit if not Draft
        public TestStatus Status { get; private set; }

        // ToDo AddQuestion, DeleteQuestion, MoveQuestion, ModifyQuestion
        public List<Question> Questions { get; private set; }

        #endregion

        #region Настройки процедуры тестирования
        public RevokePolicy RevokePolicy { get; private set; } = new RevokePolicy(false);

        public TimeSpan? TimeLimit { get; private set; }

        // MAYBE uint? заменить на RetryPolicy(uint attemptsPerTest, bool infinite)
        public uint? AttemptsLimit { get; private set; } = AttemptsLimitForTestMustBeInRangeRule.MIN_ATTEMPTS_PER_TEST;
        
        public NavigationMode NavigationMode { get; private set; } = NavigationMode.SEQUENTIONAL;
        #endregion

        #region Настройки оценивания
        public CheckingPolicy CheckingPolicy { get; private set; } = CheckingPolicy.STANDARD;

        public WorkCheckingMethod WorkCheckingMethod { get; private set; } = WorkCheckingMethod.MANUAL;

        // MAYBE ScoreDistributionMethod + PassingScore = ScorePolicy(passingScore, method)
        public uint PassingScore { get; private set; } = default;
        
        public ScoreDistributionMethod ScoreDistributionMethod { get; private set; } = ScoreDistributionMethod.AUTOMATIC;

        public uint ScorePerQuestion { get; private set; } = QuestionScoreMustBeInRange.MIN_SCORE;
        #endregion

        #region Конструкторы
        private Test() {}

        public Test(string title, string topic, int authorID, ITestCounter counter)
        {
            // TBD ChangeTitleAndTopic - правило похожее, но ошибка другая
            CheckRule(new TestMustBeUniqueRule(title, topic, INITIAL_VERSION, authorID, counter));
            Title = title;
            Topic = topic;
            Version = INITIAL_VERSION;
            AuthorID = authorID;
        }
        #endregion

        #region Методы

        public void ChangeTitleAndTopic(string title, string topic, ITestCounter counter)
        {
            // TBD Конструктор - правило похожее, но ошибка другая
            CheckRule(new TestMustBeUniqueRule(title, topic, INITIAL_VERSION, AuthorID, counter));
            Title = title;
            Topic = topic;
        }

        public void ChangeTimeLimit(TimeSpan limit)
        {
            CheckRule(new TimeLimitMustBeInRangeRule(limit));
            TimeLimit = limit;
        }

        public void ChangeRevokePolicy(RevokePolicy revokePolicy)
        {
            RevokePolicy = revokePolicy;
        }

        public void ChangeAttemptsLimit(uint? attemptsPerTest)
        {
            CheckRule(new AttemptsLimitForTestMustBeInRangeRule(attemptsPerTest));
            AttemptsLimit = attemptsPerTest;
        }

        public void ChangeNavigationMode(NavigationMode navigationMode)
        {
            NavigationMode = navigationMode;
        }

        public void ChangeCheckingPolicy(CheckingPolicy checkingPolicy)
        {
            CheckingPolicy = checkingPolicy;
        }

        public void ChangeWorkCheckingMethod(WorkCheckingMethod workCheckingMethod)
        {
            CheckRule(new QuestionsWithDetailedAnswersCannotBeCheckedAutomaticallyRule(workCheckingMethod, Questions));
            WorkCheckingMethod = workCheckingMethod;
        }

        public void ChangePassingScore(uint passingScore)
        {
            PassingScore = passingScore;
        }

        public void ChangeScoreDistributionMethod(ScoreDistributionMethod scoreDistributionMethod)
        {
            // ToDo нельзя использовать автоматическое распределение, если баллы за вопросы разные
            // MAYBE изменить подход, сделать перезапись баллов; тогда нужна правка документации
            ScoreDistributionMethod = scoreDistributionMethod;
        }

        public void ChangeScorePerQuestion(uint scorePerQuestion)
        {
            // ToDo допустимо только при автоматическом распределении
            // ToDo перезапись баллов всех вопросов
            CheckRule(new QuestionScoreMustBeInRange(scorePerQuestion));
            ScorePerQuestion = scorePerQuestion;
        }
        #endregion
    }
}
