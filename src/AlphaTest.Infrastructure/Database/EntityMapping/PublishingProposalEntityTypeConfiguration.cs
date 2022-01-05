using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests.Publishing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Database.EntityMapping
{
    public class PublishingProposalEntityTypeConfiguration : IEntityTypeConfiguration<PublishingProposal>
    {
        public void Configure(EntityTypeBuilder<PublishingProposal> builder)
        {
            builder.ToTable("PublishingProposals");
            builder.HasKey(p => p.ID);
            builder.HasOne<Test>()
                .WithMany()
                .HasForeignKey(p => p.TestID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<AlphaTestUser>()
                .WithMany()
                .HasForeignKey(p => p.AssigneeID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.SentAt).IsRequired();
            builder.Property(p => p.Status)
                .HasConversion(
                    proposalStatus => proposalStatus.ID,
                    proposalStatusID => ProposalStatus.ParseFromID(proposalStatusID)
                )
                .IsRequired();
        }
    }
}
