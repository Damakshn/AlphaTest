using AlphaTest.Core.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class ExactNumericAnswerEntityTypeConfiguration : IEntityTypeConfiguration<ExactNumericAnswer>
    {
        public void Configure(EntityTypeBuilder<ExactNumericAnswer> builder)
        {
            builder.ToTable("Answers");
            builder.Property(a => a.Value).HasColumnName("ExactNumericAnswer");
        }
    }
}
