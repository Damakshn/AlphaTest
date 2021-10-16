using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        private static string DATABASE_NAME => "AlphaTest";

        // TODO добавить в документацию пояснение - для чего используется такая схема
        public AlphaTestContext(string login, string password) :
            base(BuildOptions(login, password))
        {

        }

        private static DbContextOptions BuildOptions(string login, string password)
        {
            DbContextOptionsBuilder<AlphaTestContext> builder = new();

            builder.UseSqlServer(BuildConnectionString(login, password));
            builder.EnableSensitiveDataLogging();
            return builder.Options;
        }

        private static string BuildConnectionString(string login, string password)
        {
            SqlConnectionStringBuilder builder = new();
            builder.InitialCatalog = DATABASE_NAME;
            string serverAddress = VariableStorage.ReadVariable(VariableStorage.DatabaseServer);

            builder.DataSource = serverAddress;
            builder.UserID = login;
            builder.Password = password;
            return builder.ConnectionString;
        }
        
    }
}
