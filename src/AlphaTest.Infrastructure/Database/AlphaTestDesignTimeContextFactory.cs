using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlphaTest.Infrastructure.Database
{
    public class AlphaTestDesignTimeContextFactory : IDesignTimeDbContextFactory<AlphaTestContext>
    {   
        public AlphaTestDesignTimeContextFactory(){ }

        public AlphaTestContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();
            string login = configuration["ALPHATEST:MIGRATOR_LOGIN"];
            string password = configuration["ALPHATEST:MIGRATOR_PASSWORD"];
            string server = configuration["ALPHATEST:SERVER"];
            return new AlphaTestContext(login, password, server);
        }
    }
}
