using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Attempts;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class AttemptQueryExtensions
    {
        public static IQueryable<Attempt> Aggregates(this DbSet<Attempt> query)
        {
            return query;
        }
    }
}
