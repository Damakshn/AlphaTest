using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class QuestionScoreMustBeInRange : IBusinessRule
    {
        public static readonly int MIN_SCORE = 1;

        public static readonly int MAX_SCORE = 100;

        private readonly int _score;

        public QuestionScoreMustBeInRange(int score)
        {
            _score = score;
        }

        public string Message => $"Балл за вопрос должен быть в интервале от {MIN_SCORE} до {MAX_SCORE}";

        public bool IsBroken => _score < MIN_SCORE || _score > MAX_SCORE;
    }
}
