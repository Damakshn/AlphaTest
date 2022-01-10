using Microsoft.AspNetCore.Identity;
using System;

namespace AlphaTest.Core.Users
{
    public class AlphaTestUserRole : IdentityUserRole<Guid>
    {
        public AlphaTestUser User { get; set; }

        public AlphaTestRole Role { get; set; }
    }
}
