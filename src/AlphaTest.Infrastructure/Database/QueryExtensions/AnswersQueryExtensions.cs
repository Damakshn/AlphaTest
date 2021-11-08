using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Answers;
using System.Threading.Tasks;
using System;

namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class AnswersQueryExtensions
    {
        public static IQueryable<Answer> Aggregates(this DbSet<Answer> query)
        {
            return query;
        }

        public static async Task<Answer> GetLastActiveAnswerForQuestion(this IQueryable<Answer> query, Guid workID, Guid questionID)
        {
            return await query
                .Where(a =>
                    a.WorkID == workID &&
                    a.QuestionID == questionID &&
                    a.IsRevoked == false
                )
                .SingleOrDefaultAsync();
        }

        public static async Task<uint> GetNumberOfRetriesUsed(this IQueryable<Answer> query, Guid workID, Guid questionID)
        {
            return (uint)await query
                .Where(a =>
                    a.WorkID == workID &&
                    a.QuestionID == questionID)
                .CountAsync();
        }
    }
}
