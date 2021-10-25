using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTitleAndTopic
{
    public class ChangeTitleAndTopicUseCaseHandler : IRequestHandler<ChangeTitleAndTopicUseCaseRequest>
    {
        private AlphaTestContext _db;

        public ChangeTitleAndTopicUseCaseHandler(AlphaTestContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(ChangeTitleAndTopicUseCaseRequest request, CancellationToken cancellationToken)
        {
            bool testAlreadyExists = _db.Tests.Aggregates().Any(t => t.Title == request.Title && t.Topic == request.Topic);
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeTitleAndTopic(request.Title, request.Topic, testAlreadyExists);
            _db.SaveChanges();
            return Unit.Value;
            
        }
    }
}
