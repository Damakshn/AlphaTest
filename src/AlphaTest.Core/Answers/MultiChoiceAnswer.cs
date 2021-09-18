using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Answers.Rules;

namespace AlphaTest.Core.Answers
{
    public class MultiChoiceAnswer: Answer<MultiChoiceQuestion, List<int>>
    {
        private MultiChoiceAnswer(): base() {}

        public MultiChoiceAnswer(int id, MultiChoiceQuestion question, Attempt attempt, List<int> value)
            :base(id, question, attempt, value)
        {
            CheckRule(new MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule(question, value));
        }
    }
}
