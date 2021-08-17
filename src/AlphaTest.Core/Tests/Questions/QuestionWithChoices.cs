using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question
    {   
        public List<QuestionOption> Options { get; protected set; }
        
        protected QuestionWithChoices(): base() { }

        protected QuestionWithChoices(string text, uint number, uint score, List<QuestionOption> options) : base(text, number, score) 
        {
            CheckRule(new NumberOfOptionsForQiestionCannotBeToBig(options));
            CheckSpecificRulesForOptions(options);
            Options = options;
        }

        internal void ChangeAttributes(string text, uint score, List<QuestionOption> options)
        {
            CheckRulesForTextAndScore(text, score);
            CheckSpecificRulesForOptions(options);
            Text = text;
            Score = score;
            Options = options;
        }

        protected abstract void CheckSpecificRulesForOptions(List<QuestionOption> options);

    }
}
