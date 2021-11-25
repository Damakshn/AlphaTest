using AlphaTest.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace AlphaTest.Infrastructure.Auth.UserManagement
{
    public class AppRoleStore : RoleStore<AppRole, AlphaTestContext, Guid, AppUserRole, IdentityRoleClaim<Guid>>
    {
        public AppRoleStore(AlphaTestContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
