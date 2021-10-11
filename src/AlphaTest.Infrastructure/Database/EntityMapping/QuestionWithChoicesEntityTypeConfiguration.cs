using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class QuestionWithChoicesEntityTypeConfiguration<TQuestionWithChoices> 
        : IEntityTypeConfiguration<TQuestionWithChoices> where TQuestionWithChoices : QuestionWithChoices
    {
        public void Configure(EntityTypeBuilder<TQuestionWithChoices> builder)
        {
            builder.ToTable("Questions");
            builder
                .HasMany(q => q.Options)
                .WithOne()
                .HasForeignKey(op => op.QuestionID);
        }
    }
}
