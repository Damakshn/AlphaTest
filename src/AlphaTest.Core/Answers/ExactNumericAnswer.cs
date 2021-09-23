using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactNumericAnswer: Answer
    {
        private ExactNumericAnswer() :base() { }

        public ExactNumericAnswer(QuestionWithNumericAnswer question, Attempt attempt, decimal value)
            :base(attempt, question)
        {
            Value = value;
        }

        public decimal Value { get; private set; }
    }
}
