using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class TestQueryExtensions
    {
        public static IQueryable<Test> Aggregates(this DbSet<Test> query)
        {
            return query;
        }
    }
}
