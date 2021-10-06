using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext: DbContext
    {
        #region Свойства
        private static string DATABASE_NAME => "AlphaTest";
        #endregion

        #region Конструкторы
        // TODO добавить в документацию пояснение - для чего используется такая схема
        internal AlphaTestContext(string login, string password):
            base(BuildOptions(login, password))
        {

        }
        #endregion

        #region Методы
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(...);
        }
        

        #region Дополнительные методы для настройки контекста при создании
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
        #endregion
        #endregion
    }
}
