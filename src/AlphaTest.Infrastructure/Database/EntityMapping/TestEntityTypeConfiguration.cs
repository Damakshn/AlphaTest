using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            #region Основные свойства
            builder.ToTable("Tests");
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Title).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Topic).HasMaxLength(500).IsRequired();
            builder.Property(t => t.Version).HasDefaultValue(Test.INITIAL_VERSION);
            #endregion

            #region Собственные типы
            builder.OwnsOne(
                t => t.RevokePolicy, a => 
                {
                    a.Property(rp => rp.InfiniteRetriesEnabled).IsRequired();
                    a.Property(rp => rp.RevokeEnabled).IsRequired();
                }).Navigation(t => t.RevokePolicy).IsRequired();

            // список составителей входит в агрегат
            builder
                .HasMany<Contribution>("_contributions")
                .WithOne()
                .HasForeignKey(contribution => contribution.TestID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Много перечислений, конвертация значений
            // Хочется так:
            // https://github.com/dotnet/efcore/issues/12150
            // https://github.com/dotnet/efcore/issues/12206
            builder
                .Property(t => t.CheckingPolicy)
                .HasConversion(
                    checkingPolicy => checkingPolicy.ID,
                    id => CheckingPolicy.ParseFromID(id)
                )
                .IsRequired();

            builder
                .Property(t => t.NavigationMode)
                .HasConversion(
                    navigationMode => navigationMode.ID,
                    id => NavigationMode.ParseFromID(id)
                )
                .IsRequired();

            builder
                .Property(t => t.WorkCheckingMethod)
                .HasConversion(
                    workCheckingMethod => workCheckingMethod.ID,
                    id => WorkCheckingMethod.ParseFromID(id)
                )
                .IsRequired();

            builder
                .Property(t => t.Status)
                .HasConversion(
                    testStatus => testStatus.ID,
                    id => TestStatus.ParseFromID(id)
                )
                .IsRequired();

            builder
                .Property(t => t.ScoreDistributionMethod)
                .HasConversion(
                    scoreDistributionMethod => scoreDistributionMethod.ID,
                    id => ScoreDistributionMethod.ParseFromID(id)
                )
                .IsRequired();
            #endregion

            #region Связи с другими сущностями
            builder
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(t => t.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(t => t.Contributions);
            #endregion

            #region Прочие свойства
            builder
                .Property(t => t.ScorePerQuestion)
                .HasConversion(
                    questionScore => questionScore.Value,
                    score => new QuestionScore(score)
                )
                .IsRequired();
            #endregion

        }
    }
}
