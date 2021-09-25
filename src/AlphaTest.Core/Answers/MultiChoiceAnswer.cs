using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Answers.Rules;
using System;

namespace AlphaTest.Core.Answers
{
    public class MultiChoiceAnswer: Answer 
    {
        private MultiChoiceAnswer(): base() {}

        public MultiChoiceAnswer(MultiChoiceQuestion question, Attempt attempt, List<Guid> rightOptions)
            :base(attempt, question)
        {
            CheckRule(new MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule(question, rightOptions));
            RightOptions = rightOptions;
        }

        public List<Guid> RightOptions { get; private set; }
    }
}
