using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Questions
{
    internal class QuestionWithChoicesEntityTypeConfiguration
        : IEntityTypeConfiguration<QuestionWithChoices>
    {
        public void Configure(EntityTypeBuilder<QuestionWithChoices> builder)
        {
            builder.ToTable("Questions");
            builder
                .HasMany(q => q.Options)
                .WithOne()
                .HasForeignKey(op => op.QuestionID);
        }
    }

    internal class SingleChoiceQuestionEntityTypeConfiguration
        : IEntityTypeConfiguration<SingleChoiceQuestion>
    {
        public void Configure(EntityTypeBuilder<SingleChoiceQuestion> builder)
        {
            builder.ToTable("Questions");
            
        }
    }

    internal class MultiChoiceQuestionEntityTypeConfiguration
        : IEntityTypeConfiguration<MultiChoiceQuestion>
    {
        public void Configure(EntityTypeBuilder<MultiChoiceQuestion> builder)
        {
            builder.ToTable("Questions");

        }
    }
}
