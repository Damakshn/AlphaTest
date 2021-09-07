using System;
using System.Collections.Generic;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Publishing.Rules;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests.Ownership.Rules;

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

        public int AuthorID { get; private set; }
                
        public TestStatus Status { get; private set; } = TestStatus.Draft;

        // ToDo DeleteQuestion, MoveQuestion
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
        
        public ScoreDistributionMethod ScoreDistributionMethod { get; private set; } = ScoreDistributionMethod.MANUAL;

        public QuestionScore ScorePerQuestion { get; private set; } = new(QuestionScoreMustBeInRangeRule.MIN_SCORE);
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
            // MAYBE подумать про использование AOP, так как эта проверка используется в очень большом числе методов
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            // TBD Конструктор - правило похожее, но ошибка другая
            CheckRule(new TestMustBeUniqueRule(title, topic, INITIAL_VERSION, AuthorID, counter));
            Title = title;
            Topic = topic;
        }

        public void ChangeTimeLimit(TimeSpan limit)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new TimeLimitMustBeInRangeRule(limit));
            TimeLimit = limit;
        }

        public void ChangeRevokePolicy(RevokePolicy revokePolicy)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            RevokePolicy = revokePolicy;
        }

        public void ChangeAttemptsLimit(uint? attemptsPerTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new AttemptsLimitForTestMustBeInRangeRule(attemptsPerTest));
            AttemptsLimit = attemptsPerTest;
        }

        public void ChangeNavigationMode(NavigationMode navigationMode)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            NavigationMode = navigationMode;
        }

        public void ChangeCheckingPolicy(CheckingPolicy checkingPolicy)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckingPolicy = checkingPolicy;
        }

        public void ChangeWorkCheckingMethod(WorkCheckingMethod workCheckingMethod, IEnumerable<Question> questionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new AutomatedCheckCannotBeAppliedToQuestionsWithDetailedAnswerRule(workCheckingMethod, questionsInTest));
            WorkCheckingMethod = workCheckingMethod;
        }

        public void ChangePassingScore(uint passingScore)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            PassingScore = passingScore;
        }

        public void ConfigureScoreDistribution(ScoreDistributionMethod newScoreDistributionMethod, QuestionScore newScorePerQuestion = null)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule(newScoreDistributionMethod, newScorePerQuestion));
            ScoreDistributionMethod = newScoreDistributionMethod;
            ScorePerQuestion = newScorePerQuestion;
            // ToDo domain event
            // ToDo нужна правка документации
        }
        #endregion

        #region Работа с вопросами
        public SingleChoiceQuestion AddSingleChoiceQuestion(QuestionText text, QuestionScore score, List<QuestionOption> options, IQuestionCounter questionCounter)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            SingleChoiceQuestion question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), options);
            return question;
        }

        public MultiChoiceQuestion AddMultiChoiceQuestion(QuestionText text, QuestionScore score, List<QuestionOption> options, IQuestionCounter questionCounter)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            MultiChoiceQuestion question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), options);
            return question;
        }

        public QuestionWithDetailedAnswer AddQuestionWithDetailedAnswer(QuestionText text, QuestionScore score, IQuestionCounter questionCounter)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule(this.WorkCheckingMethod));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            QuestionWithDetailedAnswer question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score));
            return question;
        }

        public QuestionWithTextualAnswer AddQuestionWithTextualAnswer(QuestionText text,
            QuestionScore score, string rightAnswer, IQuestionCounter questionCounter)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            QuestionWithTextualAnswer question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), rightAnswer);
            return question;
        }

        public QuestionWithNumericAnswer AddQuestionWithNumericAnswer(QuestionText text,
            QuestionScore score, decimal rightAnswer, IQuestionCounter questionCounter)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = questionCounter.GetNumberOfQuestionsInTest(this.ID) + 1;
            QuestionWithNumericAnswer question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), rightAnswer);
            return question;
        }

        internal QuestionScore CalculateActualQuestionScore(QuestionScore customScore)
        {
            CheckRule(new UnifiedScoreCannotBeReplacedWithDifferentScoreRule(ScoreDistributionMethod, ScorePerQuestion, customScore));
            return this.ScoreDistributionMethod == ScoreDistributionMethod.UNIFIED
                ? this.ScorePerQuestion
                : customScore;
        }
        #endregion

        #region Публикация
        public PublishingProposal ProposeForPublishing(List<Question> allQuestionsInTest)
        {
            CheckRule(new OnlyDraftTestsCanBeProposedForPublishingRule(this.Status));
            CheckRule(new QuestionListMustNotBeEmptyBeforePublishingRule(allQuestionsInTest));
            CheckRule(new PassingScoreMustBeAchievableRule(PassingScore, allQuestionsInTest));
            Status = TestStatus.WaitingForPublishing;
            return new PublishingProposal(this.ID);
        }

        public void Publish(PublishingProposal approvedProposal)
        {
            CheckRule(new TestMustBeProposedForPublishingBeforeBeingPublishedRule(this.Status));
            CheckRule(new ProposalMustBeProvidedForPublishingRule(approvedProposal));
            CheckRule(new PublishingOfTestRequiresApprovedProposalRule(approvedProposal));
            Status = TestStatus.Published;
        }
        #endregion

        #region Авторство
        public void SwitchAuthor(User newAuthor)
        {
            CheckRule(new OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(newAuthor));
            CheckRule(new SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(newAuthor));
            AuthorID = newAuthor.ID;
        }
        #endregion

        #endregion
    }
}
