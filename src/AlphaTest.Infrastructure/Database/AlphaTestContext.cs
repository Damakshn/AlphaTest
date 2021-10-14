using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Groups;

namespace AlphaTest.Infrastructure.Database
{   
    public partial class AlphaTestContext : 
        IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, 
            AppUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
       
        #region DbSets
        private DbSet<Test> _tests { get; set; }

        private DbSet<Question> _questions { get; set; }

        private DbSet<Group> _groups { get; set; }

        private DbSet<Examination> _examinations { get; set; }

        private DbSet<Attempt> _attempts { get; set; }

        private DbSet<Answer> _answers { get; set; }

        private DbSet<CheckResult> _results { get; set; }
        #endregion

        #region Свойства
        private static string DATABASE_NAME => "AlphaTest";
        #endregion

        #region Конструкторы
        // TODO добавить в документацию пояснение - для чего используется такая схема
        public AlphaTestContext(string login, string password):
            base(BuildOptions(login, password))
        {
            Aggregates = new(this);
        }
        #endregion

        #region Методы
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyEntityConfigurations(modelBuilder);
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
