using System;
using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question
    {   
        public List<QuestionOption> Options { get; protected set; }
        
        protected QuestionWithChoices(): base() { }

        protected QuestionWithChoices(
            Guid testID,
            QuestionText text,
            uint number,
            QuestionScore score,
            List<(string text, uint number, bool isRight)> optionsData)
            : base(testID, text, number, score)
        {
            CheckCommonRulesForOptions(optionsData);
            CheckSpecificRulesForOptions(optionsData);
            List<QuestionOption> options =
                optionsData
                    .Select(o => new QuestionOption(this.ID, o.text, o.number, o.isRight))
                        .ToList();
            Options = options;
        }

        public void ChangeOptions(List<(string text, uint number, bool isRight)> newOptionsData)
        {
            CheckCommonRulesForOptions(newOptionsData);
            CheckSpecificRulesForOptions(newOptionsData);
            List<QuestionOption> options =
                newOptionsData
                    .Select(o => new QuestionOption(this.ID, o.text, o.number, o.isRight))
                        .ToList();
            Options = options;
        }

        protected abstract void CheckSpecificRulesForOptions(List<(string text, uint number, bool isRight)> optionsData);

        private void CheckCommonRulesForOptions(List<(string text, uint number, bool isRight)> optionsData)
        {
            CheckRule(new QuestionOptionsMustBeSpecifiedRule(optionsData));
            CheckRule(new NumberOfOptionsForQiestionMustBeInRangeRule(optionsData));
        }

    }
}
