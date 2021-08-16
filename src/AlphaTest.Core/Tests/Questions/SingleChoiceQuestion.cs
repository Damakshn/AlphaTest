using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions.Rules;


namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(string text, uint number, uint score, List<QuestionOption> options):
            base(text, number, score, options){ }

        protected override void CheckSpecificRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(options));
        }
    }
}
