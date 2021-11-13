using AlphaTest.Core.Common;
using System;
using System.Linq;


namespace AlphaTest.Core.Works.Rules
{
    public class ForcedFinishRequiresSpecificFinishReasonsRule : IBusinessRule
    {
        private readonly WorkFinishReason _reason;

        public ForcedFinishRequiresSpecificFinishReasonsRule(WorkFinishReason reason)
        {
            _reason = reason;
        }

        public string Message => $"Для принудительного завершения тестирования допустимы 2 причины - истекло время тестирования или завершился экзамен, указанная причина - {_reason}";

        public bool IsBroken => WorkFinishReason.ForceEndReasons.Contains(_reason) == false;
    }
}
