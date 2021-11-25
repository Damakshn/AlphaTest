using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AlphaTest.Infrastructure.Database
{
    public class AlphaTestDesignTimeContextFactory : IDesignTimeDbContextFactory<AlphaTestContext>
    {
        private IConfiguration _configuration;
        
        public AlphaTestDesignTimeContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AlphaTestContext CreateDbContext(string[] args)
        {
            return new AlphaTestContext(_configuration);
        }
    }
}
