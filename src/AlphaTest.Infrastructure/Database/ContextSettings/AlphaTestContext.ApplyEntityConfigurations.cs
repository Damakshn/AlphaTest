using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {  
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExamParticipationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptEntityTypeConfiguration());
            
        }
    }
}
