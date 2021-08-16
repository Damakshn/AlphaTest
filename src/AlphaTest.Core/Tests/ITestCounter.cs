namespace AlphaTest.Core.Tests
{
    public interface ITestCounter
    {
        int GetQuantityOfTests(string title, string topic, int version, int authorID);
    }
}
