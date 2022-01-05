using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Auth.Security;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class UserManagementPluginExtensions
    {
        public static void AddConfiguredUserManagement(this IServiceCollection services)
        {
            services.AddIdentity<AlphaTestUser, AlphaTestRole>(options =>
                {
                    options.Password = SecuritySettings.PasswordOptions;
                }
            );
            services.AddTransient<IUserStore<AlphaTestUser>, AppUserStore>();
            services.AddTransient<IRoleStore<AlphaTestRole>, AppRoleStore>();
            services.AddScoped<UserManager<AlphaTestUser>, AppUserManager>();
        }
    }
}
