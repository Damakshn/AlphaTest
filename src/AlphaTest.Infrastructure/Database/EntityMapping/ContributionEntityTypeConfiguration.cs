using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    internal class ContributionEntityTypeConfiguration : IEntityTypeConfiguration<Contribution>
    {
        public void Configure(EntityTypeBuilder<Contribution> builder)
        {
            builder.ToTable("Contributions");
            builder.HasKey(c => new { c.TestID, c.TeacherID });
            builder.HasOne<Test>().WithMany("_contributions").HasForeignKey(c => c.TestID);
            builder.HasOne<AppUser>().WithMany().HasForeignKey(c => c.TeacherID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
