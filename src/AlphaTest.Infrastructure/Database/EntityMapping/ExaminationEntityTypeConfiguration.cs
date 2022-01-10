using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class ExaminationEntityTypeConfiguration : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            builder.ToTable("Examinations");
            builder.HasKey(e => e.ID);
            builder.HasOne<Test>().WithMany().HasForeignKey(e => e.TestID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<AlphaTestUser>().WithMany().HasForeignKey(e => e.ExaminerID).OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.StartsAt).IsRequired();
            builder.Property(e => e.EndsAt).IsRequired();
            builder.Property(e => e.IsCanceled).IsRequired();

            // список групп, сдающих экзамен, входит в агрегат
            builder.OwnsMany<ExamParticipation>(
                "_participations", y =>
                {
                    y.WithOwner().HasForeignKey(p => p.ExaminationID);
                    y.HasOne<Group>().WithMany().HasForeignKey(p => p.GroupID).OnDelete(DeleteBehavior.Restrict);
                    y.HasKey(p => new { p.ExaminationID, p.GroupID });
                    y.ToTable("ExamParticipation");
                });
            builder.Ignore(e => e.Participations);
        }
    }
}
