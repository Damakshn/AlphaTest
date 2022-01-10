using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Users;


namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class CheckResultEntityTypeConfiguration : IEntityTypeConfiguration<CheckResult>
    {
        public void Configure(EntityTypeBuilder<CheckResult> builder)
        {
            builder.ToTable("Results");
            builder.HasKey(r => r.ID);
            builder
                .HasOne<Answer>()
                .WithMany()
                .HasForeignKey(r => r.AnswerID)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne<AlphaTestUser>()
                .WithMany()
                .HasForeignKey(r => r.TeacherID);
            builder
                .Property(r => r.Type)
                .HasConversion(
                    checkResultType => checkResultType.ID,
                    resultTypeID => CheckResultType.ParseFromID(resultTypeID)
                )
                .IsRequired();
            builder
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("getdate()")
                .IsRequired();
            builder.Property(r => r.Score).IsRequired();
        }
    }
}
