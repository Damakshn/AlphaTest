using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Infrastructure.Auth
{   
    public class AppUserStore : UserStore<AppUser, AppRole, AlphaTestContext, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>
    {
        public AppUserStore(AlphaTestContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        #region Методы поиска пользователей
        public override Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = ConvertIdFromString(userId);
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public Task<AppUser> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public override Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        public override Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Context.Users.Aggregates().SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        protected override Task<AppUser> FindUserAsync(Guid userId, CancellationToken cancellationToken)
        {   
            return Context.Users.Aggregates().SingleOrDefaultAsync(u => u.Id.Equals(userId), cancellationToken);
        }
        #endregion
    }
}
