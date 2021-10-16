using AlphaTest.Core.Tests.Questions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class QuestionQueryExtensions
    {
        public static IQueryable<Question> Aggregates(this IQueryable<Question> query)
        {
            return query
                .Include(q => (q as SingleChoiceQuestion).Options)
                .Include(q => (q as MultiChoiceQuestion).Options);
        }
    }
}
