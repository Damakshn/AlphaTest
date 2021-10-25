using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class QuestionQueryExtensions
    {
        public static IQueryable<Question> Aggregates(this DbSet<Question> query)
        {
            return query
                .Include(q => (q as SingleChoiceQuestion).Options)
                .Include(q => (q as MultiChoiceQuestion).Options);
        }

        public static IQueryable<Question> FilterByTest(this IQueryable<Question> query, Guid testID)
        {
            return query.Where(q => q.TestID == testID);
            
        }
    }
}
