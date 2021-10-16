using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Examinations;


namespace AlphaTest.Infrastructure.Database.QueryExtensions
{
    public static class ExaminationQueryExtensions
    {
        public static IQueryable<Examination> Aggregates(this DbSet<Examination> query)
        {
            return query;
        }
    }
}
