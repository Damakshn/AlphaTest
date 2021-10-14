using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Identity
{
    internal class AppUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            builder
                .HasOne(userRole => userRole.User)
                .WithMany("_userRoles")
                .HasForeignKey(userRole => userRole.UserId)
                .IsRequired();

            builder
                .HasOne(userRole => userRole.Role)
                .WithMany()
                .HasForeignKey(userRole => userRole.RoleId)
                .IsRequired();
        }
    }
}
