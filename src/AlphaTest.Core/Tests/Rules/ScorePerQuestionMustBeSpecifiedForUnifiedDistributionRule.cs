using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.Tests.Rules
{
    public class ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule : IBusinessRule
    {
        private readonly ScoreDistributionMethod _scoreDistributionMethod;

        private readonly QuestionScore _scorePerQuestion;

        public ScorePerQuestionMustBeSpecifiedForUnifiedDistributionRule(ScoreDistributionMethod scoreDistributionMethod, QuestionScore scorePerQuestion)
        {
            _scoreDistributionMethod = scoreDistributionMethod;
            _scorePerQuestion = scorePerQuestion;
        }

        public string Message => "При автоматическом распределении баллов должен быть указан единый балл для всех вопросов.";

        public bool IsBroken => _scoreDistributionMethod == ScoreDistributionMethod.UNIFIED && _scorePerQuestion is null;
    }
}
