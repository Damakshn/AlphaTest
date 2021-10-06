using Microsoft.EntityFrameworkCore.Design;

namespace AlphaTest.Infrastructure.Database
{
    public class AlphaTestDesignTimeContextFactory : IDesignTimeDbContextFactory<AlphaTestContext>
    {
        public AlphaTestContext CreateDbContext(string[] args)
        {   
            string migratorLogin = VariableStorage.ReadVariable(VariableStorage.MigratorLogin);
            string migratorPassword = VariableStorage.ReadVariable(VariableStorage.MigratorPassword);

            return new AlphaTestContext(migratorLogin, migratorPassword);
        }
    }
}
