using System;

namespace AlphaTest.Application.Models.Users
{
    public class StudentListItemDto
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string LastNameAndInitials { get; set; }

        public string Email { get; set; }

    }
}
