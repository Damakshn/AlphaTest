using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Core.Answers.Rules;
using System;

namespace AlphaTest.Core.Answers
{
    public class SingleChoiceAnswer: Answer
    {
        private SingleChoiceAnswer() :base() { }

        public SingleChoiceAnswer(SingleChoiceQuestion question, Work work, Guid rightOptionID)
            : base(work, question)
        {
            CheckRule(new SingleChoiceAnswerValueMustBeValidOptionIDRule(question, rightOptionID));
            RightOptionID = rightOptionID;
        }

        public Guid RightOptionID { get; private set; }
    }
}
