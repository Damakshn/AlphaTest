using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.TestSettings.TestFlow.Rules;

namespace AlphaTest.Core.Tests.TestSettings.TestFlow
{
    public class RevokePolicy: ValueObject
    {
        public bool RevokeEnabled { get; private set; }

        public uint RetriesLimit { get; private set; }

        public bool InfiniteRetriesEnabled { get; private set; }

        private RevokePolicy() {}

        public RevokePolicy(bool revokeEnabled, uint retriesLimit = 0, bool infiniteRetriesEnabled = false)
        {
            CheckRule(new RevokeMustBeAllowedIfLimitIsGreaterThanZeroRule(
                revokeEnabled, 
                retriesLimit, 
                infiniteRetriesEnabled));
            RevokeEnabled = revokeEnabled;
            RetriesLimit = retriesLimit;
            InfiniteRetriesEnabled = infiniteRetriesEnabled;
        }
    }
}
