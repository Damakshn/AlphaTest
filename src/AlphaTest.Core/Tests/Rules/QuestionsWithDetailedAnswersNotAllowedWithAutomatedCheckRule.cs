using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.Tests.Rules
{
    public class QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule : IBusinessRule
    {
        private readonly WorkCheckingMethod _checkingMethod;

        public QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule(WorkCheckingMethod checkingMethod)
        {           
            _checkingMethod = checkingMethod;
        }

        public string Message => "Вопрос с развёрнутым ответом нельзя добавить в тест с автоматической проверкой.";

        public bool IsBroken => _checkingMethod == WorkCheckingMethod.AUTOMATIC;
    }
}
