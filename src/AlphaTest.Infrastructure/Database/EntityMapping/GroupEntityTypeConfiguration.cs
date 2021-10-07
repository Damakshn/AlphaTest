using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Groups;


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
            builder.Ignore(g => g.Memberships);
        }
    }
}
