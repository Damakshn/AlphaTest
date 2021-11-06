using AlphaTest.Core.Common;

namespace AlphaTest.Core.Works.Rules
{
    public class FinishedWorkCannotBeModifiedRule : IBusinessRule
    {
        private readonly Work _work;

        public FinishedWorkCannotBeModifiedRule(Work work)
        {
            _work = work;
        }
        public string Message => "Тестирование уже завершено, операция невозможна.";

        public bool IsBroken => _work.IsFinished;
    }
}
