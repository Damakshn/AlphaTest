using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Questions
{
    internal class QuestionOptionEntityTypeConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.ToTable("QuestionOptions");
            builder.HasKey(op => op.ID);
            builder.Property(op => op.Text).IsRequired();
            builder.Property(op => op.Number).IsRequired();
        }
    }
}
