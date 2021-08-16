using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.TestSettings.TestFlow.Rules
{
    public class RevokeMustBeAllowedIfLimitIsGreaterThanZeroRule : IBusinessRule
    {
        private bool _revokeEnabled;

        private uint _retriesLimit;

        private bool _infiniteRetriesEnabled;

        public RevokeMustBeAllowedIfLimitIsGreaterThanZeroRule(
            bool revokeEnabled, 
            uint retriesLimit, 
            bool infiniteRetries)
        {
            _revokeEnabled = revokeEnabled;
            _retriesLimit = retriesLimit;
            _infiniteRetriesEnabled = infiniteRetries;
        }

        public string Message => "Если количество попыток отлично от 0, то отзыв ответа должен быть разрешён.";

        public bool IsBroken => (_revokeEnabled == false) && (_retriesLimit > 0 || _infiniteRetriesEnabled);
    }
}
