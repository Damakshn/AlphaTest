using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Core.Answers.Rules;
using System;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers
{
    public class SingleChoiceAnswer: Answer
    {
        private SingleChoiceAnswer() :base() { }

        public SingleChoiceAnswer(SingleChoiceQuestion question, Work work, Test test, uint answersAccepted, Guid rightOptionID)
            : base(work, question, test, answersAccepted)
        {
            CheckRule(new SingleChoiceAnswerValueMustBeValidOptionIDRule(question, rightOptionID));
            RightOptionID = rightOptionID;
        }

        public Guid RightOptionID { get; private set; }
    }
}
