using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactTextualAnswer: Answer<QuestionWithTextualAnswer, string>
    {
        private ExactTextualAnswer() : base() { }

        public ExactTextualAnswer(int id, QuestionWithTextualAnswer question, Attempt attempt, string value)
            : base(id, question, attempt, value)
        {

        }
    }
}
