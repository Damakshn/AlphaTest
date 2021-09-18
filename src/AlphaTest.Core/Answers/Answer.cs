using System;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.Answers
{
    public abstract class Answer<TQuestion, TValue>: Entity where TQuestion: Question
    {

        #region Конструкторы
        protected Answer() { }

        public Answer(int id, TQuestion question, Attempt attempt, TValue value)
        {
            CheckRule(new AnswerCannotBeRegisteredIfAttemptIsFinishedRule(attempt));
            ID = id;
            QuestionID = question.ID;
            AttemptID = attempt.ID;
            Value = value;
            IsRevoked = false;
            RevokedAt = null;
        }
        #endregion

        #region Свойства
        public int ID { get; private set; }

        public int QuestionID { get; private set; }

        public int AttemptID { get; private set; }

        public DateTime SentAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public TValue Value { get; private set; }

        public bool IsRevoked { get; private set; }
        #endregion

        #region Методы
        public void Revoke(Test test, Attempt attempt, int retriesUsed)
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
