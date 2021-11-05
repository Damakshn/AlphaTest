using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactNumericAnswer: Answer
    {
        private ExactNumericAnswer() :base() { }

        public ExactNumericAnswer(QuestionWithNumericAnswer question, Work work, decimal value)
            :base(work, question)
        {
            Value = value;
        }

        public decimal Value { get; private set; }
    }
}
