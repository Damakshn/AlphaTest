using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Questions
{
    internal class SingleChoiceAnswerEntityTypeConfiguration : IEntityTypeConfiguration<SingleChoiceAnswer>
    {
        public void Configure(EntityTypeBuilder<SingleChoiceAnswer> builder)
        {
            builder.ToTable("Answers");
            builder
                .HasOne<QuestionOption>()
                .WithMany()
                .HasForeignKey(a => a.RightOptionID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
