using Microsoft.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database.EntityMapping;
using AlphaTest.Infrastructure.Database.EntityMapping.Questions;
using AlphaTest.Infrastructure.Database.EntityMapping.Answers;
using AlphaTest.Infrastructure.Database.EntityMapping.Identity;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        protected void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            #region Identity
            modelBuilder.ApplyConfiguration(new AppUserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleEntityTypeConfiguration());
            #endregion

            #region Тесты, группы, экзамены, попытки
            modelBuilder.ApplyConfiguration(new TestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AttemptEntityTypeConfiguration());
            #endregion

            #region Заявки
            modelBuilder.ApplyConfiguration(new PublishingProposalEntityTypeConfiguration());
            #endregion

            #region Вопросы
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithDetailedAnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionOptionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionWithChoicesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SingleChoiceQuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MultiChoiceQuestionEntityTypeConfiguration());
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

            #region Оценки
            modelBuilder.ApplyConfiguration(new CheckResultEntityTypeConfiguration());
            #endregion

        }
    }
}
