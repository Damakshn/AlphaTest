using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers
{
    public class ExactNumericAnswer: Answer
    {
        private ExactNumericAnswer() :base() { }

        public ExactNumericAnswer(QuestionWithNumericAnswer question, Work work, Test test, uint answersAccepted, decimal value)
            :base(work, question, test, answersAccepted)
        {
            Value = value;
        }

        public decimal Value { get; private set; }
    }
}
