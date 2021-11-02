using AlphaTest.Core.Common.Exceptions;

namespace AlphaTest.Core.Common.Abstractions
{
    public abstract class Entity : ICanCheckRules
    {
        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken)
                throw new BusinessException(rule);
        }
    }
}
