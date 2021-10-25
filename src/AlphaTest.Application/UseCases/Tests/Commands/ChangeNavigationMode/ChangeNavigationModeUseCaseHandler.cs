using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeNavigationMode
{
    public class ChangeNavigationModeUseCaseHandler : IRequestHandler<ChangeNavigationModeUseCaseRequest>
    {
        private AlphaTestContext _db;

        public ChangeNavigationModeUseCaseHandler(AlphaTestContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(ChangeNavigationModeUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeNavigationMode(request.NavigationMode);
            return Unit.Value;
        }
    }
}
