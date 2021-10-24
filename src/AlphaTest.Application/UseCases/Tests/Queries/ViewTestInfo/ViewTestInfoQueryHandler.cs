using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AutoMapper;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo
{
    public class ViewTestInfoQueryHandler : IRequestHandler<ViewTestInfoQuery, TestInfo>
    {
        private AlphaTestContext _db;
        private IMapper _mapper;

        public ViewTestInfoQueryHandler(AlphaTestContext db, IMapper mapper)
        {
            _db = db;
            _db.DisableTracking();
            _mapper = mapper;
        }

        public Task<TestInfo> Handle(ViewTestInfoQuery request, CancellationToken cancellationToken)
        {
            var query = from test in _db.Tests.Aggregates().Where(t => t.ID == request.TestID)
                    join author in _db.Users.Aggregates() on test.AuthorID equals author.Id
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
                        AuthorInfo = _mapper.Map<ContributorInfo>(author)
                    };
            var result = query.FirstOrDefault();
            return Task.FromResult(result);
        }
    }
}
