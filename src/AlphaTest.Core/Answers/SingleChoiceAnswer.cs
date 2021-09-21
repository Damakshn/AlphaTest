using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.Answers
{
    public class SingleChoiceAnswer: Answer
    {
        private SingleChoiceAnswer() :base() { }

        public SingleChoiceAnswer(int id, SingleChoiceQuestion question, Attempt attempt, int rightOptionID)
            : base(id, attempt, question)
        {
            CheckRule(new SingleChoiceAnswerValueMustBeValidOptionIDRule(question, rightOptionID));
            RightOptionID = rightOptionID;
        }

        public int RightOptionID { get; private set; }
    }
}
