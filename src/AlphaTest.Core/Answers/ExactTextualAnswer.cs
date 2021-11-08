using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers
{
    public class ExactTextualAnswer: Answer
    {
        private ExactTextualAnswer() : base() { }

        public ExactTextualAnswer(QuestionWithTextualAnswer question, Work work, Test test, uint answersAccepted, string value)
            : base(work, question, test, answersAccepted)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
