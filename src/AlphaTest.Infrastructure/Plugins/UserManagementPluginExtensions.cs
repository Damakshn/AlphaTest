using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Infrastructure.Auth;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class UserManagementPluginExtensions
    {
        public static void AddConfiguredUserManagement(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>();
            services.AddTransient<IUserStore<AppUser>, AppUserStore>();
            services.AddTransient<IRoleStore<AppRole>, AppRoleStore>();
            services.AddScoped<UserManager<AppUser>, AppUserManager>();
        }
    }
}
