using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        private static string DATABASE_NAME => "AlphaTest";

        // TODO добавить в документацию пояснение - для чего используется такая схема
        public AlphaTestContext(string login, string password, string server) :
            base(BuildOptions(login, password, server))
        {
            
        }

        private static DbContextOptions BuildOptions(string login, string password, string server)
        {
            DbContextOptionsBuilder<AlphaTestContext> builder = new();
            
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
