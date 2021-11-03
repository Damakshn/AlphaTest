using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionTextMustMeSpecifiedRule : IBusinessRule
    {
        private readonly QuestionText _text;
        public QuestionTextMustMeSpecifiedRule(QuestionText text)
        {
            _text = text;
        }

        public string Message => "Текст вопроса должен быть указан.";

        public bool IsBroken => _text is null;
    }
}
