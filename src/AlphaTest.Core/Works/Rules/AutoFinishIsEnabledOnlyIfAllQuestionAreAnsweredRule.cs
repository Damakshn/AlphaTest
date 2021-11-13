using AlphaTest.Core.Common;

namespace AlphaTest.Core.Works.Rules
{
    public class AutoFinishIsEnabledOnlyIfAllQuestionAreAnsweredRule : IBusinessRule
    {
        private readonly bool _allQuestionsAnswered;

        public AutoFinishIsEnabledOnlyIfAllQuestionAreAnsweredRule(bool allQuestionsAnswered)
        {
            _allQuestionsAnswered = allQuestionsAnswered;
        }

        public string Message => "Тестирование можно завершить автоматически только после того, приняты ответы на все вопросы";

        public bool IsBroken => _allQuestionsAnswered == false;
    }
}
