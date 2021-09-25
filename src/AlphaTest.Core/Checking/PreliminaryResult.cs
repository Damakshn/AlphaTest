using AlphaTest.Core.Answers;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Checking
{
    public class PreliminaryResult: ValueObject
    {

        public PreliminaryResult (PreliminaryResult other)
        {
            Score = other.Score;
            CheckResultType = other.CheckResultType;
            Question = other.Question;
            Answer = other.Answer;
        }

        public PreliminaryResult(Question question, Answer answer, decimal score, CheckResultType checkResultType)
        {
            // ToDo add checks
            // revoked answer not allowed
            Score = score;
            CheckResultType = checkResultType;
            Question = question;
            Answer = answer;
        }

        public decimal Score { get; init; }

        public CheckResultType CheckResultType { get; init; }

        public Question Question { get; private set; }

        public Answer Answer { get; private set; }

        public QuestionScore FullScore => Question.Score;
    }
}
