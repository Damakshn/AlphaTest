using System;
using Microsoft.AspNetCore.Identity;

namespace AlphaTest.Infrastructure.Auth
{
    public class AppRole : IdentityRole<Guid>
    {
        public string NameInRussian { get; set; }
    }
}
