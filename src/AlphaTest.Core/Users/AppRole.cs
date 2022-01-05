using System;
using Microsoft.AspNetCore.Identity;


namespace AlphaTest.Core.Users
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string name, string nameInRussian) : base()
        {
            Name = name;
            NormalizedName = name.ToUpper();
            NameInRussian = nameInRussian;
        }

        public string NameInRussian { get; set; }
    }
}
