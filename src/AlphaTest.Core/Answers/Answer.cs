using System;
using AlphaTest.Core.Answers.Rules;
using AlphaTest.Core.Works;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Core.Answers
{
    public abstract class Answer: Entity
{
        #region Конструкторы
        protected Answer() { }

        protected Answer(Work work, Question question, Test test, uint answersAccepted)
        {
            CheckRule(new AnswerCannotBeRegisteredIfWorkIsFinishedRule(work));
            CheckRule(new AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule(test, answersAccepted));
            ID = Guid.NewGuid();
            QuestionID = question.ID;
            WorkID = work.ID;
            SentAt = TimeResolver.CurrentTime;
            IsRevoked = false;
            RevokedAt = null;
        }
        #endregion

        #region Свойства
        public Guid ID { get; private set; }

        public Guid QuestionID { get; private set; }

        public Guid WorkID { get; private set; }

        public DateTime SentAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool IsRevoked { get; private set; }
        #endregion

        #region Методы
        public void Revoke(Test test, Work work)
        {
            CheckRule(new AnswerCannotBeRevokedIfRevokeIsNotAllowedRule(test));
            // ToDo добавить в документацию
            CheckRule(new AnswerCannotBeRevokedIfWorkIsFinishedRule(work));
            IsRevoked = true;
            RevokedAt = TimeResolver.CurrentTime;
        }
        #endregion
    }
}
