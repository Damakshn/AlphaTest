using AlphaTest.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Identity
{
    internal class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AlphaTestRole>
    {
        public void Configure(EntityTypeBuilder<AlphaTestRole> builder)
        {
            builder.Property(r => r.NameInRussian).HasMaxLength(256);
        }
    }
}
