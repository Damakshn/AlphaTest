using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class TextualRightAnswerCannotBeNullOrWhitespaceRule : IBusinessRule
    {
        private readonly string _rightAnswer;

        public TextualRightAnswerCannotBeNullOrWhitespaceRule(string rightAnswer)
        {
            _rightAnswer = rightAnswer;
        }

        public string Message => "Правильный ответ на вопрос не может быть пустым.";

        public bool IsBroken => string.IsNullOrWhiteSpace(_rightAnswer);
    }
}
