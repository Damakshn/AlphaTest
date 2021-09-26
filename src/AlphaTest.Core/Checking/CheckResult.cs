using System;
using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Checking
{
    public class CheckResult: Entity
    {
        public CheckResult(AdjustedResult adjustedResult, int? teacherID = null)
        {
            ID = Guid.NewGuid();
            TeacherID = teacherID;
            CreatedAt = DateTime.Now;
            AnswerID = adjustedResult.AnswerID;
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
