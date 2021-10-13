using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class MultiChoiceAnswerEntityTypeConfiguration : IEntityTypeConfiguration<MultiChoiceAnswer>
    {
        public void Configure(EntityTypeBuilder<MultiChoiceAnswer> builder)
        {
            builder.ToTable("Answers");
                        
            builder.Ignore(a => a.RightOptions);

            builder.OwnsMany<ChosenOption>("_chosenOptions", y =>
                {
                    y.ToTable("ChosenOptions");
                    y.HasKey(o => new { o.AnswerID, o.OptionID });
                    y.WithOwner().HasForeignKey(o => o.AnswerID);
                    y.HasOne<QuestionOption>()
                        .WithMany()
                        .HasForeignKey(o => o.OptionID)
                        .OnDelete(DeleteBehavior.Restrict);
                }
            );
        }
    }
}
