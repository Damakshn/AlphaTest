using AlphaTest.Core.Tests.Questions.Rules;
using System.Collections.Generic;

namespace AlphaTest.Core.Tests.Questions
{
    public class MultiChoiceQuestion: QuestionWithChoices
    {
        private MultiChoiceQuestion() : base() { }

        internal MultiChoiceQuestion(int testID, string text, uint number, uint score, List<QuestionOption> options) :
            base(testID, text, number, score, options){ }

        protected override void CheckSpecificRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new AtLeastOneQuestionOptionMustBeRightRule(options));
        }
    }
}
