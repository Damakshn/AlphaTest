using AlphaTest.Core.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaTest.Core.Users.BulkImportReport.Rules;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Core.Users.BulkImportReport
{
    public class BulkImportReportLine : Entity
    {
        // для EF
        private BulkImportReportLine()
        {

        }

        // TBD подумать над тем, как сделать отчёт более информативным
        public BulkImportReportLine(string content, BulkImportEventType eventType)
        {
            // ToDo unit test this
            CheckRule(new BulkImportReportContentMustBeProvidedRule(content));
            CheckRule(new BulkImportReportEventTypeMustBeProvidedRule(eventType));
            ID = Guid.NewGuid();
            Timestamp = TimeResolver.CurrentTime;
            Content = content;
            EventType = eventType;
        }

        public Guid ID { get; private set; }

        public string Content { get; private set; }

        public DateTime Timestamp { get; private set; }

        public BulkImportEventType EventType { get; private set; }
    }
}
