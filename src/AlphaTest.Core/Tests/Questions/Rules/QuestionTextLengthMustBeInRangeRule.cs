using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionTextLengthMustBeInRangeRule : IBusinessRule
    {
        public static readonly int MIN_LENGTH = 10;
        public static readonly int MAX_LENGTH = 5000;
        private readonly string _questionText;

        public QuestionTextLengthMustBeInRangeRule(string questionText)
        {
            _questionText = questionText;
        }

        public string Message => $"Текст вопроса может быть длиной от {MIN_LENGTH} до {MAX_LENGTH} символов.";

        public bool IsBroken => _questionText.Length > MAX_LENGTH || _questionText.Length < MIN_LENGTH;
    }
}
