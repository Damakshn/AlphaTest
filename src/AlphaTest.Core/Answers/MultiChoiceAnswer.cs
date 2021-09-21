using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.Answers
{
    public class MultiChoiceAnswer: Answer 
    {
        private MultiChoiceAnswer(): base() {}

        public MultiChoiceAnswer(int id, MultiChoiceQuestion question, Attempt attempt, List<int> rightOptions)
            :base(id, attempt, question)
        {
            CheckRule(new MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule(question, rightOptions));
            RightOptions = rightOptions;
        }

        public List<int> RightOptions { get; private set; }
    }
}
