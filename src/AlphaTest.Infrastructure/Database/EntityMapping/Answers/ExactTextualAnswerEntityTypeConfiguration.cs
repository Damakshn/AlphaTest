using AlphaTest.Core.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class ExactTextualAnswerEntityTypeConfiguration : IEntityTypeConfiguration<ExactTextualAnswer>
    {
        public void Configure(EntityTypeBuilder<ExactTextualAnswer> builder)
        {
            builder.ToTable("Answers");
            builder.Property(a => a.Value).HasColumnName("ExactTextualAnswer");
        }
    }
}
