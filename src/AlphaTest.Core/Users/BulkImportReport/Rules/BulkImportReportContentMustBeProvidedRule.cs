using AlphaTest.Core.Common;

namespace AlphaTest.Core.Users.BulkImportReport.Rules
{
    public class BulkImportReportContentMustBeProvidedRule : IBusinessRule
    {
        private readonly string _content;

        public BulkImportReportContentMustBeProvidedRule(string content)
        {
            _content = content;
        }

        public string Message => "Содержание отчёта должно быть заполнено.";

        public bool IsBroken => string.IsNullOrWhiteSpace(_content);
    }
}
