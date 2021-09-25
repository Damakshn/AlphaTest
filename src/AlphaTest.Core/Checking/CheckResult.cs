using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Answers;

namespace AlphaTest.Core.Checking
{
    public class CheckResult: Entity
    {
        public CheckResult(Answer answer, int? teacherID, PreliminaryResult adjustedResult)
        {
            // ToDo answer must not be revoked
            ID = Guid.NewGuid();
            AnswerID = answer.ID;
            TeacherID = teacherID;
            CreatedAt = DateTime.Now;
            Type = adjustedResult.CheckResultType;
            Score = adjustedResult.Score;
        }

        public Guid ID { get; private set; }

        public Guid AnswerID { get; private set; }

        public int? TeacherID { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public CheckResultType Type { get; private set; }

        public decimal Score { get; private set; }

    }
}
