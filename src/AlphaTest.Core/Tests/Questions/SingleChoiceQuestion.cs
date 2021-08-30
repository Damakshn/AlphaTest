using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions.Rules;


namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(int testID, QuestionText text, uint number, QuestionScore score, List<QuestionOption> options):
            base(testID, text, number, score, options){ }

        protected override void CheckSpecificRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(options));
        }
    }
}
