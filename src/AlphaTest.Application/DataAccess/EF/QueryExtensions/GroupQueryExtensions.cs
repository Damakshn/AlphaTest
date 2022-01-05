using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Groups;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Text;
using AlphaTest.Application.DataAccess.Exceptions;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class GroupQueryExtensions
    {
        public static IQueryable<Group> Aggregates(this DbSet<Group> query)
        {
            return query.Include("_members");
        }

        public static async Task<List<Group>> FindManyByIds(this IQueryable<Group> query, List<Guid> ids)
        {
            var result = await query.Where(g => ids.Contains(g.ID)).ToListAsync();

            #region Проверка на остутствующие группы, выброс исключения
            var existingGroups = result.Select(g => g.ID).ToList();
            var missingGroups = ids.Except(existingGroups).ToList();
            if (missingGroups.Any())
            {
                string message;
                if (missingGroups.Count == 1)
                    message = $"Ошибка - группа с ID={missingGroups[0]} не найдена.";
                else
                {
                    StringBuilder builder = new();
                    builder.Append("Ошибка - группы со следующими ID не были найдены:");
                    foreach(var groupID in missingGroups)
                    {
                        builder.Append(groupID);
                        if (missingGroups.IndexOf(groupID) < missingGroups.Count - 1)
                            builder.Append(";\n");
                        else
                            builder.Append('.');
                    }
                    message = builder.ToString();
                }
                throw new EntityNotFoundException(message);
            }
            #endregion

            return result;
        }

        public static IQueryable<Group> FilterByIdsList(this IQueryable<Group> query, List<Guid> ids)
        {
            return query.Where(g => ids.Contains(g.ID));
        }

        public static async Task<Group> FindByID(this IQueryable<Group> query, Guid id)
        {
            Group group = await query.FirstOrDefaultAsync(g => g.ID == id);
            if (group is null)
                throw new EntityNotFoundException($"Группа с ID={id} не найдена.");
            return group;
        }
    }
}
