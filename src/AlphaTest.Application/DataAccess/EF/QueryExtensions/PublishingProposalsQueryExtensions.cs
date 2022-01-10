using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Application.DataAccess.Exceptions;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class PublishingProposalsQueryExtensions
    {
        public static IQueryable<PublishingProposal> Aggregates(this DbSet<PublishingProposal> query)
        {
            return query;
        }

        public static async Task<PublishingProposal> FindByID(this IQueryable<PublishingProposal> query, Guid id)
        {
            PublishingProposal proposal = await query.FirstOrDefaultAsync(t => t.ID == id);
            if (proposal is null)
                throw new EntityNotFoundException($"Заявка на публикацию с ID={id} не найдена.");
            return proposal;
        }
    }
}
