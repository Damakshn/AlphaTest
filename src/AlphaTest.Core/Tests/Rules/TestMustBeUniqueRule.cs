using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class TestMustBeUniqueRule : IBusinessRule
    {
        private readonly bool _testAlreadyExists;

        public TestMustBeUniqueRule(bool testAlreadyExists)
        {
            _testAlreadyExists = testAlreadyExists;
        }

        public string Message => "Такой тест уже есть. Придумайте другое название или тему, либо создайте новую редакцию.";

        public bool IsBroken => _testAlreadyExists;

        
    }
}
