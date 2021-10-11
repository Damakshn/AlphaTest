using AlphaTest.Core.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class DetailedAnswerEntityTypeConfiguration : IEntityTypeConfiguration<DetailedAnswer>
    {
        public void Configure(EntityTypeBuilder<DetailedAnswer> builder)
        {
            builder.ToTable("Answers");
            builder.Property(a => a.Value).HasColumnName("DetailedAnswer");
        }
    }
}
