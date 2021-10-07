using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping.Enumerations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Infrastructure.Database.EntityMapping;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
           
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionEntityTypeConfiguration());
        }
    }
}
