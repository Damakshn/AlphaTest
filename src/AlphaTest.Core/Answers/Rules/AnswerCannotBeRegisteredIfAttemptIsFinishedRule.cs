using AlphaTest.Core.Common;
using AlphaTest.Core.Attempts;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRegisteredIfAttemptIsFinishedRule : IBusinessRule
    {
        private readonly Attempt _attempt;

        public AnswerCannotBeRegisteredIfAttemptIsFinishedRule(Attempt attempt)
        {
            _attempt = attempt;
        }

        public string Message => "Невозможно зарегистрировать ответ - тестирование уже завершилось.";

        public bool IsBroken => _attempt.IsFinished;
    }
}
