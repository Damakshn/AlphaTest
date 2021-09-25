using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Answers;

namespace AlphaTest.Core.Checking
{
    public class CheckResult: Entity
    {
        public CheckResult(int id, Answer answer, int? teacherID, CheckResultType type, decimal score)
        {
            // ToDo answer must not be revoked
            ID = id;
            AnswerID = answer.ID;
            TeacherID = teacherID;
            CreatedAt = DateTime.Now;
            Type = type;
            Score = score;
        }

        public int ID { get; private set; }

        public Guid AnswerID { get; private set; }

        public int? TeacherID { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public CheckResultType Type { get; private set; }

        public decimal Score { get; private set; }

    }
}
