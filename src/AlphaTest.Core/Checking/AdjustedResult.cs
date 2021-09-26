using AlphaTest.Core.Common.Abstractions;
using System;

namespace AlphaTest.Core.Checking
{
    public class AdjustedResult: ValueObject
    {
        internal AdjustedResult(Guid answerID, decimal score, CheckResultType checkResultType)
        {
            AnswerID = answerID;
            Score = score;
            CheckResultType = checkResultType;
        }

        internal AdjustedResult(PreliminaryResult preliminaryResult)
        {
            AnswerID = preliminaryResult.Answer.ID;
            Score = preliminaryResult.FullScore.Value;
            CheckResultType = preliminaryResult.CheckResultType;
        }

        public Guid AnswerID { get; private set; }

        public decimal Score { get; private set; }

        public CheckResultType CheckResultType { get; private set; }
    }
}
