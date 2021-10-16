using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Groups;


namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class GroupQueryExtensions
    {
        public static IQueryable<Group> Aggregates(this DbSet<Group> query)
        {
            return query;
        }
    }
}
