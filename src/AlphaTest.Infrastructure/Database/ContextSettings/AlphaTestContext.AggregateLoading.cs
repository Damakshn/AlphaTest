using System.Linq;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Infrastructure.Database
{
    public partial class AlphaTestContext
    {   
        public AggregatesLoading Aggregates { get; private set; }

        public class AggregatesLoading
        {
            private AlphaTestContext _context;

            internal AggregatesLoading(AlphaTestContext context)
            {
                _context = context;
            }

            public IQueryable<AppUser> AppUsers => _context.Users.Include("_userRoles.Role");

            public IQueryable<Test> Tests => _context._tests;

            public IQueryable<Question> Questions => 
                _context._questions
                    .Include(q => (q as SingleChoiceQuestion).Options)
                    .Include(q => (q as MultiChoiceQuestion).Options);

            public IQueryable<Group> Groups => _context._groups;

            public IQueryable<Examination> Examinations => _context._examinations;

            public IQueryable<Attempt> Attempts => _context._attempts;

            public IQueryable<Answer> Answers => _context._answers;

            public IQueryable<CheckResult> Results => _context._results;
        }
    }
}
