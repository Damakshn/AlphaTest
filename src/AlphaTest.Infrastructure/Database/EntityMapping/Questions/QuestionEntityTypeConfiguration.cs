using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Questions
{
    internal class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.ID);
            builder
                .HasOne<Test>()
                .WithMany()
                .HasForeignKey(q => q.TestID);
            builder
                .Property(q => q.Text)
                .HasConversion(
                    questionText => questionText.Value, 
                    text => new QuestionText(text))
                .HasMaxLength(5000)
                .IsRequired();
            builder
                .Property(q => q.Score)
                .HasConversion(
                    questionScore => questionScore.Value,
                    value => new QuestionScore(value))
                .IsRequired();
            builder.HasDiscriminator();
            
        }
    }
}
