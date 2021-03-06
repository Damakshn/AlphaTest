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
using AlphaTest.Core.Tests.Ownership;
using System.Linq;

namespace AlphaTest.Core.Tests
{
    public class Test: Entity
    {
        public static readonly int INITIAL_VERSION = 1;

        private List<Contribution> _contributions;

        #region Основные атрибуты
        public Guid ID { get; private set; }

        public string Title { get; private set; }

        public string Topic { get; private set; }
        
        public int Version { get; private set; }

        public Guid AuthorID { get; private set; }

        public IReadOnlyCollection<Contribution> Contributions => _contributions.AsReadOnly();
                
        public TestStatus Status { get; private set; } = TestStatus.Draft;

        // ToDo DeleteQuestion, MoveQuestion
        #endregion

        #region Настройки процедуры тестирования
        public RevokePolicy RevokePolicy { get; private set; }

        public TimeSpan? TimeLimit { get; private set; }

        // MAYBE uint? заменить на RetryPolicy(uint attemptsPerTest, bool infinite)
        public uint? AttemptsLimit { get; private set; }
        
        public NavigationMode NavigationMode { get; private set; }
        #endregion

        #region Настройки оценивания
        public CheckingPolicy CheckingPolicy { get; private set; }

        public WorkCheckingMethod WorkCheckingMethod { get; private set; }

        // MAYBE ScoreDistributionMethod + PassingScore = ScorePolicy(passingScore, method)
        public uint PassingScore { get; private set; }
        
        public ScoreDistributionMethod ScoreDistributionMethod { get; private set; }

        public QuestionScore ScorePerQuestion { get; private set; }
        #endregion

        #region Конструкторы
        private Test() {}

        public Test(string title, string topic, Guid authorID, bool testAlreadyExists)
        {
            // TBD ChangeTitleAndTopic - правило похожее, но ошибка другая
            CheckRule(new TestMustBeUniqueRule(testAlreadyExists));
            ID = Guid.NewGuid();
            Title = title;
            Topic = topic;
            Version = INITIAL_VERSION;
            AuthorID = authorID;
            RevokePolicy = new RevokePolicy(false);
            TimeLimit = null;
            AttemptsLimit = AttemptsLimitForTestMustBeInRangeRule.MIN_ATTEMPTS_PER_TEST;
            NavigationMode = NavigationMode.SEQUENTIONAL;
            CheckingPolicy = CheckingPolicy.STANDARD;
            WorkCheckingMethod = WorkCheckingMethod.MANUAL;
            PassingScore = default;
            ScoreDistributionMethod = ScoreDistributionMethod.MANUAL;
            ScorePerQuestion = new QuestionScore(QuestionScoreMustBeInRangeRule.MIN_SCORE);
            _contributions = new List<Contribution>();
        }
        #endregion

        #region Методы

        #region Изменение настроек
        public void ChangeTitleAndTopic(string title, string topic, bool attributesAlreadyInUse)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            // TBD Конструктор - правило похожее, но ошибка другая
            CheckRule(new TestMustBeUniqueRule(attributesAlreadyInUse));
            Title = title;
            Topic = topic;
        }

        public void ChangeTimeLimit(TimeSpan? limit)
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
        public SingleChoiceQuestion AddSingleChoiceQuestion(
            QuestionText text,
            QuestionScore score,
            List<(string text, uint number, bool isRight)> optionsData,
            uint numberOfQuestionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = numberOfQuestionsInTest + 1;
            SingleChoiceQuestion question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), optionsData);
            return question;
        }

        public MultiChoiceQuestion AddMultiChoiceQuestion(
            QuestionText text,
            QuestionScore score,
            List<(string text, uint number, bool isRight)> optionsData,
            uint numberOfQuestionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = numberOfQuestionsInTest + 1;
            MultiChoiceQuestion question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), optionsData);
            return question;
        }

        public QuestionWithDetailedAnswer AddQuestionWithDetailedAnswer(QuestionText text, QuestionScore score, uint numberOfQuestionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            CheckRule(new QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule(this.WorkCheckingMethod));
            uint questionNumber = numberOfQuestionsInTest + 1;
            QuestionWithDetailedAnswer question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score));
            return question;
        }

        public QuestionWithTextualAnswer AddQuestionWithTextualAnswer(QuestionText text,
            QuestionScore score, string rightAnswer, uint numberOfQuestionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = numberOfQuestionsInTest + 1;
            QuestionWithTextualAnswer question = new(this.ID, text, questionNumber, CalculateActualQuestionScore(score), rightAnswer);
            return question;
        }

        public QuestionWithNumericAnswer AddQuestionWithNumericAnswer(QuestionText text,
            QuestionScore score, decimal rightAnswer, uint numberOfQuestionsInTest)
        {
            CheckRule(new NonDraftTestCannotBeEditedRule(this));
            uint questionNumber = numberOfQuestionsInTest + 1;
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
        public void SwitchAuthor(IAlphaTestUser newAuthor)
        {
            CheckRule(new OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(newAuthor));
            CheckRule(new SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(newAuthor));
            // если новый автор был составителем, убираем его оттуда
            if (_contributions.Any(c => c.TeacherID == newAuthor.Id))
            {
                Contribution newAuthorContribution = _contributions.First(c => c.TeacherID == newAuthor.Id);
                _contributions.Remove(newAuthorContribution);
            }
            // переводим старого автора в составители
            // так как пользователь уже был автором теста, то мы предполагаем, что он преподаватель и не заблокирован
            // если же нет, то контроль доступа всё равно не пропустит его
            Contribution contribution = new(this, AuthorID);
            _contributions.Add(contribution);
            AuthorID = newAuthor.Id;
        }

        public bool IsAuthor(Guid userID)
        {
            return this.AuthorID == userID;
        }

        public bool IsContributor(Guid userID)
        {
            // ToDo использовать этот метод в проверках бизнес-правил
            return this._contributions.Any(contribution => contribution.TeacherID == userID);
        }

        public void AddContributor(IAlphaTestUser contributor)
        {
            CheckRule(new TeacherCanBeAddedToContributorsOnlyOnceRule(contributor, this));
            CheckRule(new OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(contributor));
            CheckRule(new SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(contributor));
            Contribution contribution = new(this, contributor.Id);
            _contributions.Add(contribution);
        }

        public void RemoveContributor(Guid contributorID)
        {
            CheckRule(new NonContributorTeacherCannotBeRemovedFromContributorsRule(contributorID, this));
            Contribution contributionToRemove = _contributions.Where(c => c.TeacherID == contributorID).FirstOrDefault();
            _contributions.Remove(contributionToRemove);
        }
        #endregion

        #region Создание новой версии
        public Test Replicate()
        {
            CheckRule(new OnlyPublishedTestsCanBeReplicatedRule(this));
            Test replica = (Test)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.RevokePolicy = RevokePolicy.Replicate();
            replica._contributions = new List<Contribution>();
            foreach(var source in this._contributions)
            {
                Contribution copy = source.ReplicateForNewEdition(replica);
                replica._contributions.Add(copy);
            }
            Status = TestStatus.Archived;
            replica.Version = this.Version + 1;
            replica.Status = TestStatus.Draft;
            return replica;
        }
        #endregion

        #endregion
    }
}
