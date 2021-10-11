using AlphaTest.Core.Answers;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers");
            builder.HasKey(a => a.ID);
            builder.HasDiscriminator();
            builder
                .HasOne<Question>()
                .WithMany()
                .HasForeignKey(a => a.QuestionID);
            builder
                .HasOne<Attempt>()
                .WithMany()
                .HasForeignKey(a => a.AttemptID);
            builder.Property(a => a.SentAt).IsRequired();
            builder.Property(a => a.IsRevoked).IsRequired();
        }
    }
}
