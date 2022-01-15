using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Core.Common.Utils;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class TimeResolverPluginExtensions
    {
        public static void AddTimeResolver(this IServiceCollection services, IConfiguration configuration)
        {
            TimeResolver.SetTimeZone(configuration["ALPHATEST:TIMEZONE"]);
            TimeResolver.ResetTime();
        }
    }
}
