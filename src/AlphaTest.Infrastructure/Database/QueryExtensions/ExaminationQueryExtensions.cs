using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Examinations;
using AlphaTest.Infrastructure.Database.Exceptions;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class ExaminationQueryExtensions
    {
        public static IQueryable<Examination> Aggregates(this DbSet<Examination> query)
        {
            return query;
        }

        public static async Task<Examination> FindByID(this IQueryable<Examination> query, Guid id)
        {
            Examination examination = await query.FirstOrDefaultAsync(t => t.ID == id);
            if (examination is null)
                throw new EntityNotFoundException($"Экзамен с ID={id} не найден.");
            return examination;
        }
    }
}
