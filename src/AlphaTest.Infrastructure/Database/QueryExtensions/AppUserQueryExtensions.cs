using AlphaTest.Infrastructure.Auth;
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
    }
}
