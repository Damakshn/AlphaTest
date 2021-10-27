using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
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
    }
}
