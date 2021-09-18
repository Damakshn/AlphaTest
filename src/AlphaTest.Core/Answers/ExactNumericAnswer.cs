using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactNumericAnswer: Answer<QuestionWithNumericAnswer, decimal>
    {
        private ExactNumericAnswer() :base() { }

        public ExactNumericAnswer(int id, QuestionWithNumericAnswer question, Attempt attempt, decimal value)
            :base(id, question, attempt, value)
        {

        }
    }
}
