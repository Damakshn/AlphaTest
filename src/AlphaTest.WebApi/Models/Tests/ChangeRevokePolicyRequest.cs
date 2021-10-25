namespace AlphaTest.WebApi.Models.Tests
{
    public class ChangeRevokePolicyRequest
    {
        public bool RevokeEnabled { get; set; }

        public uint RetriesLimit { get; set; }

        public bool InfiniteRetriesEnabled { get; set; }
    }
}
