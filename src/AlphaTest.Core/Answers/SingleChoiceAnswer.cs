using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.Answers
{
    public class SingleChoiceAnswer: Answer<SingleChoiceQuestion, int>
    {
        private SingleChoiceAnswer() :base() { }

        public SingleChoiceAnswer(int id, SingleChoiceQuestion question, Attempt attempt, int value)
            : base(id, question, attempt, value)
        {
            CheckRule(new SingleChoiceAnswerValueMustBeValidOptionIDRule(question, value));
        }
    }
}
