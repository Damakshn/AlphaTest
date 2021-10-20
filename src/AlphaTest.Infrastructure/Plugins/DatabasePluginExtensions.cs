using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class DatabasePluginExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services)
        {
            services.AddScoped(sp => {
                // ToDo добавить выделенный логин и занести в переменные среды
                //var sqlLogin = VariableStorage.ReadVariable(VariableStorage.SqlLogin);
                //var sqlPassword = VariableStorage.ReadVariable(VariableStorage.SqlPassword);
                var sqlLogin = VariableStorage.ReadVariable(VariableStorage.MigratorLogin);
                var sqlPassword = VariableStorage.ReadVariable(VariableStorage.MigratorPassword);
                return new AlphaTestContext(sqlLogin, sqlPassword);
            });
        }
    }
}
