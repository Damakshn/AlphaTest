using AlphaTest.Core.Common;
using System;


namespace AlphaTest.Core.Attempts.Rules
{
    public class ForcedEndMustBeAppliedAtRightTimeRule : IBusinessRule
    {
        private readonly Attempt _attempt;

        public ForcedEndMustBeAppliedAtRightTimeRule(Attempt attempt)
        {
            _attempt = attempt;
        }

        public string Message => $"Принудительное завершение тестирования невозможно до {_attempt.ForceEndAt}";

        public bool IsBroken => _attempt.ForceEndAt > DateTime.Now;
    }
}
