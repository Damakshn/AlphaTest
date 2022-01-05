using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class QuestionQueryExtensions
    {
        public static IQueryable<Question> Aggregates(this DbSet<Question> query)
        {
            return query
                .Include(q => (q as QuestionWithChoices).Options);
        }

        public static IQueryable<Question> FilterByTest(this IQueryable<Question> query, Guid testID)
        {
            return query.Where(q => q.TestID == testID);
            
        }

        public static IQueryable<Question> SortByNumber(this IQueryable<Question> query)
        {
            return query.OrderBy(q => q.Number);
        }

        public static async Task<Question> FindByID(this IQueryable<Question> query, Guid id)
        {
            Question question = await query.FirstOrDefaultAsync(t => t.ID == id);
            if (question is null)
                throw new EntityNotFoundException($"Вопрос с ID={id} не найден.");
            return question;
        }
    }
}
