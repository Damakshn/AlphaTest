using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Identity
{
    internal class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .Property("_firstName")
                .HasMaxLength(300)
                .HasColumnName("FirstName")
                .IsRequired();
            builder
                .Property("_lastName")
                .HasMaxLength(300)
                .HasColumnName("LastName")
                .IsRequired(); ;
            builder
                .Property("_middleName")
                .HasMaxLength(300)
                .HasColumnName("MiddleName")
                .IsRequired(); ;
            builder
                .Property("_temporaryPassword")
                .HasMaxLength(50)
                .HasColumnName("TemporaryPassword")
                .IsRequired();
            builder
                .Property("_temporaryPasswordExpirationDate")
                .HasColumnName("TemporaryPasswordExpirationDate")
                .IsRequired();
            builder
                .Property("_registeredAt")
                .HasDefaultValueSql("getdate()")
                .HasColumnName("RegisteredAt")
                .IsRequired();
            builder
                .Property("_lastVisitedAt")
                .HasColumnName("LastVisitedAt");
            builder
                .Property("_isPasswordChanged")
                .HasColumnName("IsPasswordChanged")
                .IsRequired();
            builder
                .Property("_isSuspended")
                .HasColumnName("IsSuspended")
                .IsRequired();
            builder
                .HasMany<AppUserRole>("_userRoles")
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
