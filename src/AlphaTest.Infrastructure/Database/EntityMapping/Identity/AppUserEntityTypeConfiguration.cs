using AlphaTest.Infrastructure.Auth.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Identity
{
    internal class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
                .Property(user => user.FirstName)
                .HasMaxLength(300)
                .IsRequired();
            builder
                .Property(user => user.LastName)
                .HasMaxLength(300)
                .IsRequired(); ;
            builder
                .Property(user => user.MiddleName)
                .HasMaxLength(300)
                .IsRequired(); ;
            builder
                .Property(user => user.TemporaryPassword)
                .HasMaxLength(50) // ToDo validate password length
                .IsRequired();
            builder
                .Property(user => user.TemporaryPasswordExpirationDate)
                .IsRequired();
            builder
                .Property(user => user.RegisteredAt)
                .HasDefaultValueSql("getdate()") // ToDo datetime.now?
                .IsRequired();
            builder
                .Property(user => user.IsPasswordChanged)
                .IsRequired();
            builder
                .Property(user => user.IsSuspended)
                .IsRequired();
            builder
                .HasMany<AppUserRole>("_userRoles")
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
