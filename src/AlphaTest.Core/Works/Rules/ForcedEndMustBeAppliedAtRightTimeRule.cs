using AlphaTest.Core.Common;
using System;


namespace AlphaTest.Core.Works.Rules
{
    public class ForcedEndMustBeAppliedAtRightTimeRule : IBusinessRule
    {
        private readonly Work _work;

        public ForcedEndMustBeAppliedAtRightTimeRule(Work work)
        {
            _work = work;
        }

        public string Message => $"Принудительное завершение тестирования невозможно до {_work.ForceEndAt}";

        public bool IsBroken => _work.ForceEndAt > DateTime.Now;
    }
}
