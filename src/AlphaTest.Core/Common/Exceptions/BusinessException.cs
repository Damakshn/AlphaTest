using System;

namespace AlphaTest.Core.Common.Exceptions
{
    public class BusinessException: AlphaTestException
    {
        private IBusinessRule _brokenRule;

        public BusinessException(IBusinessRule brokenRule): base(brokenRule.Message)
        {
            _brokenRule = brokenRule;
        }

        public override string ToString() => $"{_brokenRule.GetType().FullName}: {_brokenRule.Message}";

    }
}
