using AlphaTest.Core.Common;

namespace AlphaTest.Core.Users.BulkImportReport.Rules
{
    public class BulkImportReportEventTypeMustBeProvidedRule : IBusinessRule
    {
        private readonly BulkImportEventType _eventType;

        public BulkImportReportEventTypeMustBeProvidedRule(BulkImportEventType eventType)
        {
            _eventType = eventType;
        }

        public string Message => "Тип записи отчёта должен быть указан.";

        public bool IsBroken => _eventType is null;
    }
}
