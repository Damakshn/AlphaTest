using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Core.Checking
{
    public class CheckResult: Entity
    {
        public CheckResult(AdjustedResult adjustedResult, Guid? teacherID = null)
        {
            ID = Guid.NewGuid();
            TeacherID = teacherID;
            CreatedAt = TimeResolver.CurrentTime;
            AnswerID = adjustedResult.AnswerID;
            Type = adjustedResult.CheckResultType;
            Score = adjustedResult.Score;
        }

        // для EF
        private CheckResult() { }

        public Guid ID { get; private set; }

        public Guid AnswerID { get; private set; }

        public Guid? TeacherID { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public CheckResultType Type { get; private set; }

        public decimal Score { get; private set; }

    }
}
