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
            return new AlphaTestContext(configuration);
        }
    }
}
