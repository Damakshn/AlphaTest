using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Works.Rules
{
    public class AutoFinishIsEnabledOnlyIfRetriesAreDisabledInTestSettingsRule : IBusinessRule
    {
        private readonly Test _test;

        public AutoFinishIsEnabledOnlyIfRetriesAreDisabledInTestSettingsRule(Test test)
        {
            _test = test;
        }
           
        public string Message => "Автоматическое завершение тестирования возможно только если составитель запретил повторно отправлять ответы или отзывать их.";

        public bool IsBroken => _test.RevokePolicy.RevokeEnabled;
    }
}
