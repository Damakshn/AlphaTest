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

        // ToDo AddQuestion, DeleteQuestion, MoveQuestion
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

        public uint ScorePerQuestion { get; private set; } = QuestionScoreMustBeInRangeRule.MIN_SCORE;
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

        #region Изменение настроек
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

        public void ChangeWorkCheckingMethod(WorkCheckingMethod workCheckingMethod, IEnumerable<Question> questionsInTest)
        {
            CheckRule(new AutomatedCheckCannotBeAppliedToQuestionsWithDetailedAnswerRule(workCheckingMethod, questionsInTest));
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
            CheckRule(new QuestionScoreMustBeInRangeRule(scorePerQuestion));
            ScorePerQuestion = scorePerQuestion;
        }
        #endregion

        #region Работа с вопросами
        public SingleChoiceQuestion AddSingleChoiceQuestion(string text, uint score, List<QuestionOption> options, IQuestionCounter questionCounter)
        {
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            // ToDo score from test
            SingleChoiceQuestion question = new(this.ID, text, questionNumber, score, options);
            return question;
        }

        public MultiChoiceQuestion AddMultiChoiceQuestion(string text, uint score, List<QuestionOption> options, IQuestionCounter questionCounter)
        {
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            // ToDo score from test
            MultiChoiceQuestion question = new(this.ID, text, questionNumber, score, options);
            return question;
        }

        public QuestionWithDetailedAnswer AddQuestionWithDetailedAnswer(string text, uint score, IQuestionCounter questionCounter)
        {
            CheckRule(new QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule(WorkCheckingMethod));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            // ToDo score from test
            QuestionWithDetailedAnswer question = new(this.ID, text, questionNumber, score);
            return question;
        }

        public QuestionWithTextualAnswer AddQuestionWithTextualAnswer(string text,
            uint score, string rightAnswer, IQuestionCounter questionCounter)
        {
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            // ToDo score from test
            QuestionWithTextualAnswer question = new(this.ID, text, questionNumber, score, rightAnswer);
            return question;
        }

        public QuestionWithNumericAnswer AddQuestionWithNumericAnswer(string text,
            uint score, decimal rightAnswer, IQuestionCounter questionCounter)
        {
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            // ToDo score from test
            QuestionWithNumericAnswer question = new(this.ID, text, questionNumber, score, rightAnswer);
            return question;
        }
        #endregion


        #endregion
    }
}
