using System;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Works.Rules
{
    public class ForcedFinishMustBeAppliedAtRightTimeRule : IBusinessRule
    {
        private readonly Work _work;

        public ForcedFinishMustBeAppliedAtRightTimeRule(Work work)
        {
            _work = work;
        }

        public string Message => $"Принудительное завершение тестирования невозможно до {_work.ForceEndAt}";

        public bool IsBroken =>_work.ForceEndAt > DateTime.Now;
    }
}
