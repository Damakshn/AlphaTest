using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class QuestionWithNumericAnswerEntityTypeConfiguration : IEntityTypeConfiguration<QuestionWithNumericAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionWithNumericAnswer> builder)
        {
            builder.ToTable("Questions");
            builder.Property(q => q.RightAnswer).HasColumnName("NumericRightAnswer");
        }
    }
}
