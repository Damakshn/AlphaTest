using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class AppUserQueryExtensions
    {
        public static IQueryable<AppUser> Aggregates(this DbSet<AppUser> query)
        {
            return query.Include("_userRoles.Role");
        }
    }
}
