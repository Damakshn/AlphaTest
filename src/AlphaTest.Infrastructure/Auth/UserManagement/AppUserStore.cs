using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AlphaTest.Core.Users;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Infrastructure.Auth.UserManagement
{
    public class AppUserStore : UserStore<AlphaTestUser, AlphaTestRole, AlphaTestContext, Guid, IdentityUserClaim<Guid>, AlphaTestUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>
    {
        public AppUserStore(AlphaTestContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        #region Методы поиска пользователей
        public override Task<AlphaTestUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var id = ConvertIdFromString(userId);
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public Task<AlphaTestUser> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public override Task<AlphaTestUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Context.Users.Aggregates().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        public override Task<AlphaTestUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Context.Users.Aggregates().SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        protected override Task<AlphaTestUser> FindUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return Context.Users.Aggregates().SingleOrDefaultAsync(u => u.Id.Equals(userId), cancellationToken);
        }
        #endregion
    }
}
