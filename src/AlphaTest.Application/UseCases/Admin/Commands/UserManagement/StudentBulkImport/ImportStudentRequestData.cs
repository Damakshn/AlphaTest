using System;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.StudentBulkImport
{
    public class ImportStudentRequestData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public Guid GroupID { get; set; }
    }
}
