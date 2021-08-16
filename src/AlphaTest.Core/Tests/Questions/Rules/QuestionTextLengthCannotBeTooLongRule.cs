using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionTextLengthCannotBeTooLongRule : IBusinessRule
    {
        public static readonly int MAX_LENGTH = 5000;
        private readonly string _questionText;

        public QuestionTextLengthCannotBeTooLongRule(string questionText)
        {
            _questionText = questionText;
        }

        public string Message => $"Длина формулировки вопроса не может превышать {MAX_LENGTH} символов.";

        public bool IsBroken => _questionText.Length > MAX_LENGTH;
    }
}
