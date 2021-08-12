namespace AlphaTest.Core.Tests.TestSettings.TestFlow
{
    public class RevokePolicy
    {   
        public bool RevokeEnabled { get; private set; }

        public uint RetriesLimit { get; private set; }

        public bool InfiniteRetriesEnabled { get; private set; }

        private RevokePolicy() {}

        public RevokePolicy(bool revokeEnabled, uint retriesLimit = 1, bool infiniteRetriesEnabled = false)
        {
            RevokeEnabled = revokeEnabled;
            RetriesLimit = retriesLimit;
            InfiniteRetriesEnabled = infiniteRetriesEnabled;
        }
    }
}
