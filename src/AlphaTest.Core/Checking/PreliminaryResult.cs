using AlphaTest.Core.Answers;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Checking.Rules;

namespace AlphaTest.Core.Checking
{
    public class PreliminaryResult: ValueObject
    {

        public PreliminaryResult(Question question, Answer answer, decimal score, CheckResultType checkResultType)
        {
            CheckRule(new RevokedAnswersCannotBeCheckedRule(answer));
            Score = score;
            CheckResultType = checkResultType;
            Question = question;
            Answer = answer;
        }

        public decimal Score { get; private set; }

        public CheckResultType CheckResultType { get; private set; }

        public Question Question { get; private set; }

        public Answer Answer { get; private set; }

        public QuestionScore FullScore => Question.Score;

        public AdjustedResult AdjustWithCheckingPolicy(CheckingPolicy checkingPolicy)
        {
            return checkingPolicy.AdjustPreliminaryResult(this);
        }
    }
}
