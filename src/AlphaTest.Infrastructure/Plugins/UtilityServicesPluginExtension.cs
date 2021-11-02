using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Checkers;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class UtilityServicesPluginExtension
    {
        public static void AddUtilityServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupUniquenessChecker, EFGroupUniquenessChecker>();
        }
    }
}
