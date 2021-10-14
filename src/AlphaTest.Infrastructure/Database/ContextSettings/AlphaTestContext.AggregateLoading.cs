using System.Linq;
using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {
        public IQueryable<AppUser> GetUsers()
        {
            return Users.Include("_userRoles.Role");
        }
    }
}
