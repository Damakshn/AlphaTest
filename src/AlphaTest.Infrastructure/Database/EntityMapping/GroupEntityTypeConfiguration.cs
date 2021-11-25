using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(g => g.ID);
            builder.Property(g => g.Name).HasMaxLength(256).IsRequired();
            builder.Property(g => g.BeginDate).IsRequired();
            builder.Property(g => g.EndDate).IsRequired();
            builder.Property(g => g.IsDisbanded).IsRequired();
            builder
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(g => g.CuratorID)
                .IsRequired(false);

            // список членов группы входит в агрегат
            builder.OwnsMany<Membership>(
                "_members", y => 
                {
                    y.WithOwner().HasForeignKey(m => m.GroupID);
                    y.HasOne<AppUser>().WithMany().HasForeignKey(m => m.StudentID);
                    y.HasKey(m => new { m.GroupID, m.StudentID });
                    y.ToTable("GroupMembership");
                });

            builder.Ignore(g => g.Memberships);
        }
    }
}
