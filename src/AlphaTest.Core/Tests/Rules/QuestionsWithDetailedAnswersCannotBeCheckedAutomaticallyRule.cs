using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Tests.Rules
{
    public class QuestionsWithDetailedAnswersCannotBeCheckedAutomaticallyRule : IBusinessRule
    {
        private readonly WorkCheckingMethod _checkingMethod;
        private readonly IEnumerable<Question> _questionsInTest;

        // ToDo Too long
        public QuestionsWithDetailedAnswersCannotBeCheckedAutomaticallyRule(
            WorkCheckingMethod checkingMethod,
            IEnumerable<Question> questionsInTest)
        {
            _checkingMethod = checkingMethod;
            _questionsInTest = questionsInTest;
        }

        public string Message => "В тесте есть вопросы с развёрнутым ответом, поэтому автоматическую проверку использовать нельзя.";

        public bool IsBroken =>
            _questionsInTest.Any(q => q is QuestionWithDetailedAnswer) &&
            _checkingMethod == WorkCheckingMethod.AUTOMATIC;
    }
}
