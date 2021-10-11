using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database.EntityMapping.Questions;
using AlphaTest.Infrastructure.Database.EntityMapping.Answers;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            #region Пользователь
            modelBuilder.ApplyConfiguration(new AppUserEntityTypeConfiguration());
            #endregion

            #region Тесты, группы, экзамены, попытки
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptEntityTypeConfiguration());
            #endregion

            #region Вопросы
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithDetailedAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithChoicesEntityTypeConfiguration<SingleChoiceQuestion>());
            modelBuilder.ApplyConfiguration(new QuestionWithChoicesEntityTypeConfiguration<MultiChoiceQuestion>());
            modelBuilder.ApplyConfiguration(new QuestionWithNumericAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithTextualAnswerEntityTypeConfiguration());
            #endregion

            #region Ответы
            modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SingleChoiceAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MultiChoiceAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExactNumericAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExactTextualAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DetailedAnswerEntityTypeConfiguration());
            #endregion
        }
    }
}
