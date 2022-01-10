using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Works;
using AlphaTest.Application.DataAccess.Exceptions;
using System.Threading.Tasks;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class WorkQueryExtensions
    {
        public static IQueryable<Work> Aggregates(this DbSet<Work> query)
        {
            return query;
        }

        public static async Task<Work> FindByID(this IQueryable<Work> query, Guid id)
        {
            Work work = await query.FirstOrDefaultAsync(t => t.ID == id);
            if (work is null)
                throw new EntityNotFoundException($"Работа с ID={id} не найдена.");
            return work;
        }

        public static async Task<Work> GetActiveWork(this IQueryable<Work> query, Guid examinationID, Guid studentID)
        {
            // MAYBE нужно обработать ситуацию с несколькими активными работами вручную? Как лучше это сделать?
            return await query
                .Where(w => 
                w.ExaminationID == examinationID && 
                w.StudentID == studentID && 
                w.FinishedAt == null)
                .SingleOrDefaultAsync();
        }
    }
}
