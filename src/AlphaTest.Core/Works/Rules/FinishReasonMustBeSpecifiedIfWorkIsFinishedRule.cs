using AlphaTest.Core.Common;

namespace AlphaTest.Core.Works.Rules
{
    public class FinishReasonMustBeSpecifiedIfWorkIsFinishedRule : IBusinessRule
    {
        private readonly WorkFinishReason _reason;

        public FinishReasonMustBeSpecifiedIfWorkIsFinishedRule(WorkFinishReason reason)
        {
            _reason = reason;
        }

        public string Message => "При завершении работы должна быть указана причина";

        public bool IsBroken => _reason is null;
    }
}
