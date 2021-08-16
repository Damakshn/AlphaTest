using AlphaTest.Core.Common.Exceptions;

namespace AlphaTest.Core.Common.Abstractions
{
    public abstract class ValueObject
    {
        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken)
                throw new BusinessException(rule);
        }
    }
}
