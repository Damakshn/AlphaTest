using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EFCore = Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Ownership;
using AlphaTest.Application.DataAccess.Exceptions;

namespace AlphaTest.Application.DataAccess.EF.QueryExtensions
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

        public static IQueryable<Test> FilterByTitle(this IQueryable<Test> query, string title)
        {
            return title is null ? query : query.Where(test => EFCore.EF.Functions.Like(test.Title, "%" + title + "%"));
        }

        public static IQueryable<Test> FilterByTopic(this IQueryable<Test> query, string topic)
        {
            return topic is null ? query : query.Where(test => EFCore.EF.Functions.Like(test.Topic, "%" + topic + "%"));
        }

        public static IQueryable<Test> FilterByAuthor(this IQueryable<Test> query, Guid? authorID)
        {
            return authorID is null ? query : query.Where(test => test.AuthorID == (Guid)authorID);
        }

        public static IQueryable<Test> FilterByStatusList(this IQueryable<Test> query, List<TestStatus> statuses)
        {   
            return statuses is null 
                ? query 
                : query.Where(test => statuses.Contains(test.Status));
        }

        public static IQueryable<Test> FilterByAuthorOrContributor(
            this IQueryable<Test> query,
            Guid? authorOrContributorID)
        {
            return authorOrContributorID is null
                ? query
                : query.Where(test => 
                    test.AuthorID == (Guid)authorOrContributorID ||
                    EFCore.EF.Property<List<Contribution>>(test, "_contributions").Any(contribution =>
                        contribution.TeacherID == (Guid)authorOrContributorID &&
                        contribution.TestID == test.ID));
        }
    }
}
