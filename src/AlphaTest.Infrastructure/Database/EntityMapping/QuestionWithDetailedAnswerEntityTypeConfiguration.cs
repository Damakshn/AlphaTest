using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class QuestionWithDetailedAnswerEntityTypeConfiguration : IEntityTypeConfiguration<QuestionWithDetailedAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionWithDetailedAnswer> builder)
        {
            builder.ToTable("Questions");
        }
    }
}
