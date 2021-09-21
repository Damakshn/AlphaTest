using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactNumericAnswer: Answer
    {
        private ExactNumericAnswer() :base() { }

        public ExactNumericAnswer(int id, QuestionWithNumericAnswer question, Attempt attempt, decimal value)
            :base(id, attempt, question)
        {
            Value = value;
        }

        public decimal Value { get; private set; }
    }
}
