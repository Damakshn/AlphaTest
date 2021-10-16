using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Checking;


namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class ResultQueryExtensions
    {
        public static IQueryable<CheckResult> Aggregates(this DbSet<CheckResult> query)
        {
            return query;
        }
    }
}
