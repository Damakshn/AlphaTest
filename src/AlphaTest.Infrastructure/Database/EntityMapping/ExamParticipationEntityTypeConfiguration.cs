using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Groups;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class ExamParticipationEntityTypeConfiguration : IEntityTypeConfiguration<ExamParticipation>
    {
        public void Configure(EntityTypeBuilder<ExamParticipation> builder)
        {
            builder.ToTable("ExamParticipation");
            builder.HasKey(ep => new { ep.ExaminationID, ep.GroupID });
            builder.HasOne<Examination>().WithMany().HasForeignKey(ep => ep.ExaminationID);
            builder.HasOne<Group>().WithMany().HasForeignKey(ep => ep.GroupID);
        }
    }
}
