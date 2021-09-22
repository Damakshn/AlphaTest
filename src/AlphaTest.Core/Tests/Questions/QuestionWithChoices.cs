using System.Collections.Generic;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question, IAutoCheckQuestion
    {   
        public List<QuestionOption> Options { get; protected set; }
        
        protected QuestionWithChoices(): base() { }

        protected QuestionWithChoices(int testID, QuestionText text, uint number, QuestionScore score, List<QuestionOption> options) 
            : base(testID, text, number, score)
        {
            CheckCommonRulesForOptions(options);
            CheckSpecificRulesForOptions(options);
            Options = options;
        }

        public void ChangeOptions(List<QuestionOption> newOptions)
        {
            CheckCommonRulesForOptions(newOptions);
            CheckSpecificRulesForOptions(newOptions);
            Options = newOptions;
        }

        protected abstract void CheckSpecificRulesForOptions(List<QuestionOption> options);

        private void CheckCommonRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new QuestionOptionsMustBeSpecifiedRule(options));
            CheckRule(new NumberOfOptionsForQiestionMustBeInRangeRule(options));
        }

        public abstract bool IsRight(Answer answer);
    }
}
