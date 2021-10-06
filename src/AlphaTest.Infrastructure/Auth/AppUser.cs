using System;
using Microsoft.AspNetCore.Identity;

namespace AlphaTest.Infrastructure.Auth
{
    public class AppUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string TemporaryPassword { get; set; }

        public DateTime TemporaryPasswordExpirationDate { get; set; }

        public bool PasswordChanged { get; set; } = false;

        public bool IsSuspended { get; set; } = false;
    }
}
