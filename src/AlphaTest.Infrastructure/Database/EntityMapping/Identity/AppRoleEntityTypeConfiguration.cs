using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaTest.Infrastructure.Database.EntityMapping.Identity
{
    internal class AppRoleEntityTypeConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(r => r.NameInRussian).HasMaxLength(256);
        }
    }
}
