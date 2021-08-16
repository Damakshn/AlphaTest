using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.Tests.Rules;
using System.Collections.Generic;

namespace AlphaTest.Core.Tests.Questions
{
    public class MultiChoiceQuestion: QuestionWithChoices
    {
        private MultiChoiceQuestion() : base() { }

        internal MultiChoiceQuestion(string text, uint number, uint score, List<QuestionOption> options) :
            base(text, number, score)
        {
            CheckRule(new AtLeastOneQuestionOptionMustBeRight(options));
            Options = options;
        }

        internal override void ChangeAttributes(string text, uint score, List<QuestionOption> options)
        {
            CheckRulesForTextAndScore(text, score);
            CheckRule(new AtLeastOneQuestionOptionMustBeRight(options));
            Text = text;
            Score = score;
            Options = options;
        }
    }
}
