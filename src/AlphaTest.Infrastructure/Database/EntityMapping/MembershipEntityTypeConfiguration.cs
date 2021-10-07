using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Auth;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class MembershipEntityTypeConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.ToTable("GroupMembership");
            builder.HasKey(m => new { m.GroupID, m.StudentID });
            builder.HasOne<Group>().WithMany("_members").HasForeignKey(m => m.GroupID);
            builder.HasOne<AppUser>().WithMany().HasForeignKey(m => m.StudentID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
