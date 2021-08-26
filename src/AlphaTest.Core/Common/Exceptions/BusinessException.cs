using System;

namespace AlphaTest.Core.Common.Exceptions
{
    public class BusinessException: AlphaTestException
    {
        public IBusinessRule BrokenRule { get; }

        public BusinessException(IBusinessRule brokenRule): base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
        }

        public override string ToString() => $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";

    }
}
