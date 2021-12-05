using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        private static string DATABASE_NAME => "AlphaTest";

        // TODO добавить в документацию пояснение - для чего используется такая схема
        public AlphaTestContext(IConfiguration configuration) :
            base(BuildOptions(configuration))
        {

        }

        private static DbContextOptions BuildOptions(IConfiguration configuration)
        {
            DbContextOptionsBuilder<AlphaTestContext> builder = new();
            string login = configuration["ALPHATEST:MIGRATOR_LOGIN"];
            string password = configuration["ALPHATEST:MIGRATOR_PASSWORD"];
            string server = configuration["ALPHATEST:SERVER"];

            builder.UseSqlServer(BuildConnectionString(login, password, server));
            builder.EnableSensitiveDataLogging();
            return builder.Options;
        }

        private static string BuildConnectionString(string login, string password, string server)
        {
            SqlConnectionStringBuilder builder = new();
            builder.InitialCatalog = DATABASE_NAME;

            builder.DataSource = server;
            builder.UserID = login;
            builder.Password = password;
            return builder.ConnectionString;
        }
        
    }
}
