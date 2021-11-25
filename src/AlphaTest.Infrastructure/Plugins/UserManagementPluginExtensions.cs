using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Auth.Security;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class UserManagementPluginExtensions
    {
        public static void AddConfiguredUserManagement(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password = SecuritySettings.PasswordOptions;
                }
            );
            services.AddTransient<IUserStore<AppUser>, AppUserStore>();
            services.AddTransient<IRoleStore<AppRole>, AppRoleStore>();
            services.AddScoped<UserManager<AppUser>, AppUserManager>();
        }
    }
}
