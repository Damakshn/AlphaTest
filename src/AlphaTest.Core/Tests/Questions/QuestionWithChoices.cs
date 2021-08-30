﻿using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question
    {   
        public List<QuestionOption> Options { get; protected set; }
        
        protected QuestionWithChoices(): base() { }

        protected QuestionWithChoices(int testID, string text, uint number, QuestionScore score, List<QuestionOption> options) 
            : base(testID, text, number, score)
        {
            CheckRule(new NumberOfOptionsForQiestionMustBeInRangeRule(options));
            CheckSpecificRulesForOptions(options);
            Options = options;
        }

        internal void ChangeAttributes(string text, QuestionScore score, List<QuestionOption> options)
        {
            CheckCommonRules(text);
            CheckSpecificRulesForOptions(options);
            Text = text;
            Score = score;
            Options = options;
        }

        protected abstract void CheckSpecificRulesForOptions(List<QuestionOption> options);

    }
}
