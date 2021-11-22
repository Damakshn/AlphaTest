using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo
{
    public class ViewTestInfoQueryHandler : UseCaseReportingHandlerBase<ViewTestInfoQuery, TestInfo>
    {   
        public ViewTestInfoQueryHandler(AlphaTestContext db, IMapper mapper):base(db, mapper) { }

        public override async Task<TestInfo> Handle(ViewTestInfoQuery request, CancellationToken cancellationToken)
        {
            // ToDo убедиться, что отстутствие составителей не схлопнет запрос
            var query = from test in _db.Tests.Where(t => t.ID == request.TestID)
                    join author in _db.Users on test.AuthorID equals author.Id
                    select new TestInfo()
                    {
                        ID = test.ID,
                        Title = test.Title,
                        Topic = test.Topic,
                        Version = test.Version,
                        Status = test.Status,
                        RevokePolicy = test.RevokePolicy,
                        TimeLimit = test.TimeLimit,
                        AttemptsLimit = test.AttemptsLimit,
                        NavigationMode = test.NavigationMode,
                        CheckingPolicy = test.CheckingPolicy,
                        WorkCheckingMethod = test.WorkCheckingMethod,
                        PassingScore = test.PassingScore,
                        ScoreDistributionMethod = test.ScoreDistributionMethod,
                        ScorePerQuestion = test.ScorePerQuestion,
                        AuthorInfo = _mapper.Map<ContributorInfo>(author),
                        ContributorsInfo = (from contributor in _db.Users 
                                            join contribution in _db.Contributions on contributor.Id equals contribution.TeacherID
                                            where contribution.TestID == test.ID
                                            select contributor).Select(contributor => _mapper.Map<ContributorInfo>(contributor)).ToList()
                    };
            return await query.FirstOrDefaultAsync();
        }
    }
}
