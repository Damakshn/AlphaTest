namespace AlphaTest.Core.Tests.TestSettings.TestFlow
{
    public class TimeLimit
    {
        public uint Hours { get; private set; }

        public uint Minutes { get; private set; }

        private TimeLimit() { }

        public TimeLimit(uint hours, uint minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }
    }
}
