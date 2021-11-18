using System.Collections.Generic;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Users.BulkImportReport;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.StudentBulkImport
{
    public class BulkImportUseCaseRequest : IUseCaseRequest<List<BulkImportReportLine>>
    {
        public BulkImportUseCaseRequest(List<ImportStudentRequestData> students)
        {
            Students = students.AsReadOnly();
        }

        public IReadOnlyCollection<ImportStudentRequestData> Students { get; private set; }
    }
}
