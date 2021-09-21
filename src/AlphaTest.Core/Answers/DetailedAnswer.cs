using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class DetailedAnswer : Answer
    {
        private DetailedAnswer() : base() { }

        public DetailedAnswer(int id, QuestionWithDetailedAnswer question, Attempt attempt, string value)
            : base(id, attempt, question)
        {
            Value = value;
        }

        public string Value { get; private set; }

    }
}
