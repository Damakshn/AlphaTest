using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Users;
using AlphaTest.Application.DataAccess.Exceptions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class AppUserQueryExtensions
    {
        public static IQueryable<AlphaTestUser> Aggregates(this DbSet<AlphaTestUser> query)
        {
            return query.Include("_userRoles.Role");
        }

        public static async Task<AlphaTestUser> FindByUsername(this IQueryable<AlphaTestUser> query, string username)
        {
            AlphaTestUser user = await query.FirstOrDefaultAsync(u => u.UserName == username);
            if (user is null)
                throw new EntityNotFoundException($"Пользователь {username} не зарегистрирован в системе.");
            return user;
        }

        public static async Task<AlphaTestUser> FindByID(this IQueryable<AlphaTestUser> query, Guid id)
        {
            AlphaTestUser user = await query.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new EntityNotFoundException($"Пользователь с ID={id} не зарегистрирован в системе.");
            return user;
        }

        public static IQueryable<AlphaTestUser> FilterByEmailsList(this IQueryable<AlphaTestUser> query, List<string> emails) 
        {
            return query.Where(u => emails.Contains(u.Email));
        }

        public static IQueryable<AlphaTestUser> FilterByFIO(this IQueryable<AlphaTestUser> query, string fio)
        {
            return
                string.IsNullOrWhiteSpace(fio)
                ? query
                : query.Where(u => EF.Functions.Like(u.LastName + " " + u.FirstName + " " + u.MiddleName, "%" + fio + "%"));
        }

        public static IQueryable<AlphaTestUser> FilterByLockStatus(this IQueryable<AlphaTestUser> query, bool? isSuspended)
        {
            return
                isSuspended is null
                ? query
                : query.Where(u => u.IsSuspended == isSuspended);
        }

        public static IQueryable<AlphaTestUser> FilterByEmail(this IQueryable<AlphaTestUser> query, string email)
        {
            return
                string.IsNullOrWhiteSpace(email)
                ? query
                : query.Where(u => EF.Functions.Like(u.Email, "%" + email + "%"));
        }

        public static IQueryable<AlphaTestUser> FilterByRoles(this IQueryable<AlphaTestUser> query, List<string> roles, IDbContext db)
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

        public static IQueryable<AlphaTestUser> FilterByGroups(this IQueryable<AlphaTestUser> query, List<Guid> groups, IDbContext db)
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

        public static IQueryable<AlphaTestUser> StudiesInGroup(this IQueryable<AlphaTestUser> query, Guid groupID, IDbContext db)
        {
            return from user in query
                   join membership in db.Memberships on user.Id equals membership.StudentID
                   join g in db.Groups on membership.GroupID equals g.ID
                   where g.ID == groupID
                   select user;
        }
    }
}
