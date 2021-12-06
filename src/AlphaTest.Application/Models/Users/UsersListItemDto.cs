using System;
using System.Collections.Generic;


namespace AlphaTest.Application.Models.Users
{
    public class UsersListItemDto
    {
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public bool IsSuspended { get; set; }

        public List<string> Groups { get; set; }
    }
}
