using Microsoft.AspNetCore.Identity;

namespace AlphaTest.Infrastructure.Auth.Security
{
    public static class SecuritySettings
    {
        public static PasswordOptions PasswordOptions =>
            new()
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true,
                RequiredLength = 8
            };
    }
}
