using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class DetailedAnswer : Answer<QuestionWithDetailedAnswer, string>
    {
        private DetailedAnswer() : base() { }

        public DetailedAnswer(int id, QuestionWithDetailedAnswer question, Attempt attempt, string value)
            : base(id, question, attempt, value)
        {

        }
    }
}
