using System.Collections.Generic;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class OnlyDraftTestsCanBeProposedForPublishingRule : IBusinessRule
    {
        private readonly TestStatus _status;

        public OnlyDraftTestsCanBeProposedForPublishingRule(TestStatus status)
        {
            _status = status;
        }

        public string Message => $"Уже опубликованные или отправленные в архив тесты не могут быть предложены для публикации.";

        public bool IsBroken => _status != TestStatus.Draft;
    }
}
