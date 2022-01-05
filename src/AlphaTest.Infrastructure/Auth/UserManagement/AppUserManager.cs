using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaTest.Core.Users;

namespace AlphaTest.Infrastructure.Auth.UserManagement
{
    public class AppUserManager : UserManager<AlphaTestUser>
    {
        public AppUserManager(IUserStore<AlphaTestUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<AlphaTestUser> passwordHasher, IEnumerable<IUserValidator<AlphaTestUser>> userValidators, IEnumerable<IPasswordValidator<AlphaTestUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<AlphaTestUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public Task<AlphaTestUser> FindByIdAsync(Guid userId)
        {
            ThrowIfDisposed();
            return (Store as AppUserStore).FindByIdAsync(userId, CancellationToken);
        }
    }
}
