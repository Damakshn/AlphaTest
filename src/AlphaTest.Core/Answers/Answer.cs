using System;
using AlphaTest.Core.Answers.Rules;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public abstract class Answer: Entity
{
        #region Конструкторы
        protected Answer() { }

        protected Answer(Attempt attempt, Question question)
        {
            CheckRule(new AnswerCannotBeRegisteredIfAttemptIsFinishedRule(attempt));
            ID = Guid.NewGuid();
            QuestionID = question.ID;
            AttemptID = attempt.ID;
            IsRevoked = false;
            RevokedAt = null;
        }
        #endregion

        #region Свойства
        public Guid ID { get; private set; }

        public Guid QuestionID { get; private set; }

        public Guid AttemptID { get; private set; }

        public DateTime SentAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool IsRevoked { get; private set; }
        #endregion

        #region Методы
        public void Revoke(Test test, Attempt attempt, uint retriesUsed)
        {
            CheckRule(new AnswerCannotBeRevokedIfRevokeIsNotAllowedRule(test));
            CheckRule(new AnswerCannotBeRevokedIfNumberOfRetriesIsExhaustedRule(test, retriesUsed));
            // ToDo добавить в документацию
            CheckRule(new AnswerCannotBeRevokedIfAttemptIsFinishedRule(attempt));
            IsRevoked = true;
            RevokedAt = DateTime.Now;
        }
        #endregion
    }
}
