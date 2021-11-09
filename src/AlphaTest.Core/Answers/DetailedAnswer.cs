using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers
{
    public class DetailedAnswer : Answer
    {
        private DetailedAnswer() : base() { }

        public DetailedAnswer(QuestionWithDetailedAnswer question, Work work, Test test, uint answersAccepted, string value)
            : base(work, question, test, answersAccepted)
        {
            Value = value;
        }

        public string Value { get; private set; }

    }
}
