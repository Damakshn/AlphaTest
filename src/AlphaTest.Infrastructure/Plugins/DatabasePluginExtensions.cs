using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Infrastructure.Database;
using Microsoft.Extensions.Configuration;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class DatabasePluginExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            string login = configuration["ALPHATEST:MIGRATOR_LOGIN"];
            string password = configuration["ALPHATEST:MIGRATOR_PASSWORD"];
            string server = configuration["ALPHATEST:SERVER"];
            services.AddScoped(sp => {
                // ToDo добавить выделенный логин и занести в переменные среды
                return new AlphaTestContext(login, password, server);
            });
        }
    }
}
