using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTitleAndTopic
{
    public class ChangeTitleAndTopicUseCaseHandler : UseCaseHandlerBase<ChangeTitleAndTopicUseCaseRequest>
    {
        public ChangeTitleAndTopicUseCaseHandler(AlphaTestContext db):base(db) { }

        public async override Task<Unit> Handle(ChangeTitleAndTopicUseCaseRequest request, CancellationToken cancellationToken)
        {
            bool testAlreadyExists = _db.Tests.Aggregates().Any(t => t.Title == request.Title && t.Topic == request.Topic);
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeTitleAndTopic(request.Title, request.Topic, testAlreadyExists);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
