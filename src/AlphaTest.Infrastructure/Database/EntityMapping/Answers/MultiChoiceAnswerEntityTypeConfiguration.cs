using AlphaTest.Core.Answers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;


namespace AlphaTest.Infrastructure.Database.EntityMapping.Answers
{
    internal class MultiChoiceAnswerEntityTypeConfiguration : IEntityTypeConfiguration<MultiChoiceAnswer>
    {
        public void Configure(EntityTypeBuilder<MultiChoiceAnswer> builder)
        {
            builder.ToTable("Answers");

            // ToDo промежуточная таблица (нужно править тесты)
            builder
                .Property(a => a.RightOptions)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(new char[] { ',' }).Select(s => Guid.Parse(s)).ToList()
                );
        }
    }
}
