using System;
using System.Linq;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;

namespace AlphaTest.Infrastructure.Checkers
{
    public class EFGroupUniquenessChecker : IGroupUniquenessChecker
    {
        private readonly AlphaTestContext _db;

        public EFGroupUniquenessChecker(AlphaTestContext db)
        {
            _db = db;
        }

        public bool CheckIfGroupExists(string name, DateTime beginDate, DateTime endDate)
        {
            return _db.Groups.Aggregates().Any(g =>
                g.Name == name && (
                g.EndDate > beginDate && g.EndDate <= endDate ||
                endDate > g.BeginDate && endDate <= g.EndDate)
            );
        }

        public bool CheckIfOneMoreGroupExists(string name, DateTime beginDate, DateTime endDate, Guid id)
        {
            return _db.Groups.Aggregates().Any(g =>
                g.ID != id &&
                g.Name == name && (
                g.EndDate > beginDate && g.EndDate <= endDate ||
                endDate > g.BeginDate && endDate <= g.EndDate)
            );
        }
    }
}
