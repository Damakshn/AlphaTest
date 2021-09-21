using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactTextualAnswer: Answer
    {
        private ExactTextualAnswer() : base() { }

        public ExactTextualAnswer(int id, QuestionWithTextualAnswer question, Attempt attempt, string value)
            : base(id, attempt, question)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
