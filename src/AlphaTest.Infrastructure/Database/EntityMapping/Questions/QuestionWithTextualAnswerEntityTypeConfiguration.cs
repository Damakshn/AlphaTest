using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Questions
{
    internal class QuestionWithTextualAnswerEntityTypeConfiguration : IEntityTypeConfiguration<QuestionWithTextualAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionWithTextualAnswer> builder)
        {
            builder.ToTable("Questions");
            builder.Property(q => q.RightAnswer).HasColumnName("TextualRightAnswer");
        }
    }
}
