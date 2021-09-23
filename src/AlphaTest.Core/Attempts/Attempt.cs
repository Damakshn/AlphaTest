using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Attempts.Rules;
using AlphaTest.Core.Examinations;

namespace AlphaTest.Core.Attempts
{
    public class Attempt: Entity
    {
        #region Конструкторы
        private Attempt(){ }

        public Attempt(int id, Test test, Examination examination, int studentID)
        {
            CheckRule(new NewAttemptCannotBeStartedIfExamIsClosedRule(examination));
            CheckRule(new NewAttemptCannotBeStartedIfExaminationIsAreadyEndedRule(examination));
            ID = id;
            ExaminationID = examination.ID;
            StartedAt = DateTime.Now;
            #region Вычисляем продолжительность тестирования
            // ToDo улучшить читаемость
            if (test.TimeLimit is null)
                ForceEndAt = StartedAt + examination.TimeRemained;
            else
            {
                var actualDuration = 
                    (TimeSpan)(test.TimeLimit < examination.TimeRemained ? test.TimeLimit : examination.TimeRemained);
                ForceEndAt = StartedAt + actualDuration;
            }
            #endregion
            StudentID = studentID;
            FinishedAt = null;
        }
        #endregion

        #region Свойства
        public int ID { get; private set; }

        public Guid ExaminationID { get; private set; }

        public int StudentID { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        public TimeSpan? Duration => FinishedAt is null ? null : FinishedAt - StartedAt;

        public DateTime ForceEndAt { get; private set; }

        public bool IsFinished => FinishedAt is not null;
        #endregion

        #region Методы
        public void Finish()
        {
            CheckRule(new FinishedAttemptCannotBeModifiedRule(this));
            FinishedAt = DateTime.Now;
        }

        public void ForceEnd()
        {   
            CheckRule(new ForcedEndMustBeAppliedAtRightTimeRule(this));
            Finish();
        }
        #endregion
    }
}
