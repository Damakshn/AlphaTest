using AlphaTest.Core.Answers;
using AlphaTest.Core.Works;
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
                .HasOne<Work>()
                .WithMany()
                .HasForeignKey(a => a.WorkID);
            builder.Property(a => a.SentAt).IsRequired();
            builder.Property(a => a.IsRevoked).IsRequired();
        }
    }
}
