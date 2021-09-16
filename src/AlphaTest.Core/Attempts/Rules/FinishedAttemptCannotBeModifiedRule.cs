using AlphaTest.Core.Common;

namespace AlphaTest.Core.Attempts.Rules
{
    public class FinishedAttemptCannotBeModifiedRule : IBusinessRule
    {
        private readonly Attempt _attempt;

        public FinishedAttemptCannotBeModifiedRule(Attempt attempt)
        {
            _attempt = attempt;
        }
        public string Message => "Тестирование уже завершено, операция невозможна.";

        public bool IsBroken => _attempt.IsFinished;
    }
}
