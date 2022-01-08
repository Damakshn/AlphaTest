using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Application.DataAccess.EF.Abstractions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class DatabasePluginExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // ToDo добавить выделенный логин и занести в переменные среды
            string login = configuration["ALPHATEST:MIGRATOR_LOGIN"];
            string password = configuration["ALPHATEST:MIGRATOR_PASSWORD"];
            string server = configuration["ALPHATEST:SERVER"];
            // В слое приложения используются 2 интерфейса - IDbContext и IDbReportingContext
            // (для обработки данных и для формирования представления, write model & read model)
            // в инфраструктурных классах используется конкретный класс AlphaTestContext
            services.AddScoped<IDbContext, AlphaTestContext>(x => new AlphaTestContext(login, password, server));
            services.AddScoped<IDbReportingContext, AlphaTestContext>(x => new AlphaTestContext(login, password, server, reportMode: true));
            services.AddScoped(sp => {
                return new AlphaTestContext(login, password, server);
            });
        }
    }
}
