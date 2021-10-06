using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping.Enumerations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Tests.Publishing;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            #region Перечисления
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<CheckResultType>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<ProposalStatus>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<CheckingPolicy>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<ScoreDistributionMethod>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<WorkCheckingMethod>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<NavigationMode>());
            modelBuilder.ApplyConfiguration(new EnumerationEntityTypeConfiguration<TestStatus>());
            #endregion
        }
    }
}
