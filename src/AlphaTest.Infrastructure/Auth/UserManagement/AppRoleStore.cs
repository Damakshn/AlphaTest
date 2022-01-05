using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AlphaTest.Core.Users;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Infrastructure.Auth.UserManagement
{
    public class AppRoleStore : RoleStore<AlphaTestRole, AlphaTestContext, Guid, AlphaTestUserRole, IdentityRoleClaim<Guid>>
    {
        public AppRoleStore(AlphaTestContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
