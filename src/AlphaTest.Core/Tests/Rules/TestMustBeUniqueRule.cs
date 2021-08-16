using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class TestMustBeUniqueRule : IBusinessRule
    {
        private readonly string _title;
        private readonly string _topic;
        private readonly int _version;
        private readonly int _authorID;
        private readonly ITestCounter _counter;

        public string Message => "Такой тест уже есть. Придумайте другое название или тему, либо создайте новую редакцию.";

        public bool IsBroken => _counter.GetQuantityOfTests(_title, _topic, _version, _authorID) > 0;

        public TestMustBeUniqueRule(string title, string topic, int version, int authorID, ITestCounter counter)
        {
            _title = title;
            _topic = topic;
            _version = version;
            _authorID = authorID;
            _counter = counter;
        }
    }
}
