using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Answers;
using System.Threading.Tasks;
using System;
using AlphaTest.Application.DataAccess.Exceptions;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
{
    public static class AnswersQueryExtensions
    {
        public static IQueryable<Answer> Aggregates(this DbSet<Answer> query)
        {
            return query;
        }

        public static async Task<Answer> FindByID(this IQueryable<Answer> query, Guid id)
        {
            Answer answer = await query.FirstOrDefaultAsync(a => a.ID == id);
            if (answer is null)
                throw new EntityNotFoundException($"Ответ с ID={id} не найден.");
            return answer;
        }

        public static async Task<Answer> GetLatestActiveAnswerForQuestion(this IQueryable<Answer> query, Guid workID, Guid questionID)
        {
            return await query
                .Where(a =>
                    a.WorkID == workID &&
                    a.QuestionID == questionID &&
                    a.IsRevoked == false
                )
                .SingleOrDefaultAsync();
        }

        public static async Task<Answer> GetLatestAnswerForQuestion(this IQueryable<Answer> query, Guid workID, Guid questionID)
        {
            return await query
                .Where(a =>
                    a.WorkID == workID &&
                    a.QuestionID == questionID
                )
                .OrderByDescending(a => a.SentAt)
                .FirstOrDefaultAsync();
        }

        public static async Task<uint> GetNumberOfAcceptedAnswers(this IQueryable<Answer> query, Guid workID, Guid questionID)
        {
            return (uint)await query
                .Where(a =>
                    a.WorkID == workID &&
                    a.QuestionID == questionID)
                .CountAsync();
        }
    }
}
