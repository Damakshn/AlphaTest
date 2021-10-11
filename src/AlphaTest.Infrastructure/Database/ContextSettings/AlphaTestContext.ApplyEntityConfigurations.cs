using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithDetailedAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithChoicesEntityTypeConfiguration<SingleChoiceQuestion>());
            modelBuilder.ApplyConfiguration(new QuestionWithChoicesEntityTypeConfiguration<MultiChoiceQuestion>());
            modelBuilder.ApplyConfiguration(new QuestionWithNumericAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithTextualAnswerEntityTypeConfiguration());

        }
    }
}
