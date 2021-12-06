using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Database.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class AppUserQueryExtensions
    {
        public static IQueryable<AppUser> Aggregates(this DbSet<AppUser> query)
        {
            return query.Include("_userRoles.Role");
        }

        public static async Task<AppUser> FindByUsername(this IQueryable<AppUser> query, string username)
        {
            AppUser user = await query.FirstOrDefaultAsync(u => u.UserName == username);
            if (user is null)
                throw new EntityNotFoundException($"Пользователь {username} не зарегистрирован в системе.");
            return user;
        }

        public static async Task<AppUser> FindByID(this IQueryable<AppUser> query, Guid id)
        {
            AppUser user = await query.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new EntityNotFoundException($"Пользователь с ID={id} не зарегистрирован в системе.");
            return user;
        }

        public static IQueryable<AppUser> FilterByEmailsList(this IQueryable<AppUser> query, List<string> emails) 
        {
            return query.Where(u => emails.Contains(u.Email));
        }

        public static IQueryable<AppUser> FilterByFIO(this IQueryable<AppUser> query, string fio)
        {
            return
                string.IsNullOrWhiteSpace(fio)
                ? query
                : query.Where(u => EF.Functions.Like(u.LastName + " " + u.FirstName + " " + u.MiddleName, "%" + fio + "%"));
        }

        public static IQueryable<AppUser> FilterByLockStatus(this IQueryable<AppUser> query, bool? isSuspended)
        {
            return
                isSuspended is null
                ? query
                : query.Where(u => u.IsSuspended == isSuspended);
        }

        public static IQueryable<AppUser> FilterByEmail(this IQueryable<AppUser> query, string email)
        {
            return
                string.IsNullOrWhiteSpace(email)
                ? query
                : query.Where(u => EF.Functions.Like(u.Email, "%" + email + "%"));
        }

        public static IQueryable<AppUser> FilterByRoles(this IQueryable<AppUser> query, List<string> roles, AlphaTestContext db)
{
            return roles.Count == 0
            ? query
            : from user in query
              where (from userRole in db.UserRoles
                     join role in db.Roles on userRole.RoleId equals role.Id
                     where userRole.UserId == user.Id
                     select role.Name).Any(roleName => roles.Any(r => r == roleName))
              select user;
        }

        public static IQueryable<AppUser> FilterByGroups(this IQueryable<AppUser> query, List<Guid> groups, AlphaTestContext db)
        {
            return groups.Count == 0
                ? query
                : from user in query
                  where (from g in db.Groups
                         join membership in db.Memberships on g.ID equals membership.GroupID
                         where
                            membership.StudentID == user.Id &&
                            groups.Any(groupID => groupID == g.ID)
                         select g.ID).Count() == groups.Count
                  select user;
        }

        public static IQueryable<AppUser> StudiesInGroup(this IQueryable<AppUser> query, Guid groupID, AlphaTestContext db)
        {
            return from user in query
                   join membership in db.Memberships on user.Id equals membership.StudentID
                   join g in db.Groups on membership.GroupID equals g.ID
                   where g.ID == groupID
                   select user;
        }
    }
}
