using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Checking
{
    public class PreliminaryResult: ValueObject
    {
        public PreliminaryResult(decimal score, CheckResultType checkResultType, QuestionScore fullScore)
        {
            // ToDo add checks
            // MAYBE передавать вопрос целиком
            Score = score;
            CheckResultType = checkResultType;
            FullScore = fullScore;
        }

        public decimal Score { get; private set; }

        public CheckResultType CheckResultType { get; private set; }

        public QuestionScore FullScore { get; private set; }
    }
}
