using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Core.Answers.Rules;
using System;
using System.Linq;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers
{
    public class MultiChoiceAnswer: Answer 
    {
        private List<ChosenOption> _chosenOptions;

        private MultiChoiceAnswer(): base() {}

        public MultiChoiceAnswer(MultiChoiceQuestion question, Work work, Test test, uint answersAccepted, List<Guid> rightOptions)
            :base(work, question, test, answersAccepted)
        {
            CheckRule(new MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule(question, rightOptions));
            _chosenOptions = rightOptions.Select(o => new ChosenOption(this.ID, o)).ToList();
        }

        public List<Guid> RightOptions => _chosenOptions.Select(o => o.OptionID).ToList();
    }
}
