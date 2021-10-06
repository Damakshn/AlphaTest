using AlphaTest.Core.Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Enumerations
{
    internal class EnumerationEntityTypeConfiguration<TEnumeration> : IEntityTypeConfiguration<TEnumeration> 
        where TEnumeration : Enumeration<TEnumeration>
    {
        public void Configure(EntityTypeBuilder<TEnumeration> builder)
        {
            builder.ToTable(typeof(TEnumeration).Name);
            builder.HasKey(e => e.ID);
            builder.Property(e => e.ID).ValueGeneratedNever();
            builder.Property(e => e.Name).HasMaxLength(256).IsRequired();
            builder.HasData(Enumeration<TEnumeration>.All);
            builder.HasIndex(e => e.ID);
            builder.HasIndex(e => e.Name).IsUnique(true);
        }
    }
}
