using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class DetailedAnswer : Answer
    {
        private DetailedAnswer() : base() { }

        public DetailedAnswer(QuestionWithDetailedAnswer question, Work work, string value)
            : base(work, question)
        {
            Value = value;
        }

        public string Value { get; private set; }

    }
}
