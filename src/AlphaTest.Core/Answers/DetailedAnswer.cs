using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class DetailedAnswer : Answer
    {
        private DetailedAnswer() : base() { }

        public DetailedAnswer(QuestionWithDetailedAnswer question, Attempt attempt, string value)
            : base(attempt, question)
        {
            Value = value;
        }

        public string Value { get; private set; }

    }
}
