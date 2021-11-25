using Microsoft.AspNetCore.Identity;
using System;

namespace AlphaTest.Infrastructure.Auth.UserManagement
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUser User { get; set; }

        public AppRole Role { get; set; }
    }
}
