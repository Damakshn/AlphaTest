using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Works;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class WorkEntityTypeConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works");
            builder.HasKey(a => a.ID);
            builder
                .HasOne<Examination>()
                .WithMany()
                .HasForeignKey(a => a.ExaminationID)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne<AlphaTestUser>()
                .WithMany()
                .HasForeignKey(a => a.StudentID);
            builder.Property(a => a.StartedAt).IsRequired();
            builder.Property(a => a.FinishedAt);
            builder.Property(a => a.ForceEndAt).IsRequired();
            builder
                .Property(w => w.FinishReason)
                .HasConversion(
                    finishReason => finishReason.ID,
                    id => WorkFinishReason.ParseFromID(id)
                )
                .IsRequired(false);
        }
    }
}
