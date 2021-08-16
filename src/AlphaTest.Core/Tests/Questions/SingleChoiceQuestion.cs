using System.Collections.Generic;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions.Rules;


namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(string text, uint number, uint score, List<QuestionOption> options):
            base(text, number, score)
        {
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(options));
            Options = options;
        }

        internal override void ChangeAttributes(string text, uint score, List<QuestionOption> options)
        {
            CheckRulesForTextAndScore(text, score);
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(options));
            Text = text;
            Score = score;
            Options = options;
        }
    }
}
