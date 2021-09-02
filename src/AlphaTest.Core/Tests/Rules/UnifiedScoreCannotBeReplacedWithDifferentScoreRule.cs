using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.Tests.Rules
{
    public class UnifiedScoreCannotBeReplacedWithDifferentScoreRule : IBusinessRule
    {
        private readonly ScoreDistributionMethod _distributionMethod;
        private readonly QuestionScore _scorePerQuestion;
        private readonly QuestionScore _score;

        public UnifiedScoreCannotBeReplacedWithDifferentScoreRule(ScoreDistributionMethod method, QuestionScore scorePerQuestion, QuestionScore score)
        {
            _distributionMethod = method;
            _scorePerQuestion = scorePerQuestion;
            _score = score;
        }

        public string Message => $"Балл за вопрос в тесте унифицирован и должен быть равен {_scorePerQuestion} (передано - {_score}).";

        public bool IsBroken => _distributionMethod == ScoreDistributionMethod.UNIFIED && _score is not null && _score != _scorePerQuestion;
    }
}
