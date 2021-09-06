using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class TestMustBeProposedForPublishingBeforeBeingPublishedRule : IBusinessRule
    {
        private readonly TestStatus _status;

        public TestMustBeProposedForPublishingBeforeBeingPublishedRule(TestStatus status)
        {
            _status = status;
        }

        // ToDo придумать более ясное сообщение
        public string Message => "Перед публикацией тест должен пройти проверку.";

        public bool IsBroken => _status != TestStatus.WaitingForPublishing;
    }
}
