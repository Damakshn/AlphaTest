using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Core.Users;


namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class ContributionEntityTypeConfiguration : IEntityTypeConfiguration<Contribution>
    {
        public void Configure(EntityTypeBuilder<Contribution> builder)
        {
            builder.ToTable("Contributions");
            builder.HasKey(contribution => new { contribution.TestID, contribution.TeacherID });
            builder
                .HasOne<AlphaTestUser>()
                .WithMany()
                .HasForeignKey(contribution => contribution.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
