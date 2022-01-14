using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Utils;

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

        public bool IsBroken =>_work.ForceEndAt > TimeResolver.CurrentTime;
    }
}
