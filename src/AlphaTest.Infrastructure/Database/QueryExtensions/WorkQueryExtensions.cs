using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Works;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class WorkQueryExtensions
    {
        public static IQueryable<Work> Aggregates(this DbSet<Work> query)
        {
            return query;
        }
    }
}
