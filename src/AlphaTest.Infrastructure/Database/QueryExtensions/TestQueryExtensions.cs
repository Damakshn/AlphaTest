using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests;
using System.Threading.Tasks;
using AlphaTest.Infrastructure.Database.Exceptions;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class TestQueryExtensions
    {
        public static IQueryable<Test> Aggregates(this DbSet<Test> query)
        {
            return query.Include("_contributions");            
        }

        public static async Task<Test> FindByID(this IQueryable<Test> query, Guid id)
        {
            Test test = await query.FirstOrDefaultAsync(t => t.ID == id);
            if (test is null)
                throw new EntityNotFoundException($"Тест с ID={id} не найден.");
            return test;
        }
    }
}
