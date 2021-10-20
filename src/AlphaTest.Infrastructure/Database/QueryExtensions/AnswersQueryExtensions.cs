using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Answers;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class AnswersQueryExtensions
    {
        public static IQueryable<Answer> Aggregates(this DbSet<Answer> query)
        {
            return query;
        }
    }
}
