using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Answers;

namespace AlphaTest.Core.Checking
{
    public class CheckResult: Entity
    {
        public CheckResult(PreliminaryResult adjustedResult, int? teacherID = null)
        {
            ID = Guid.NewGuid();
            AnswerID = adjustedResult.Answer.ID;
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
