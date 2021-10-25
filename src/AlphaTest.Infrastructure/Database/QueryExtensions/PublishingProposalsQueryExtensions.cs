using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests.Publishing;


namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class PublishingProposalsQueryExtensions
    {
        public static IQueryable<PublishingProposal> Aggregates(this DbSet<PublishingProposal> query)
        {
            return query;
        }
    }
}
