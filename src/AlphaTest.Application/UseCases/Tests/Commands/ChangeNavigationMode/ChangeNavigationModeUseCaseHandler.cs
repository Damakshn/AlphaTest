using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeNavigationMode
{
    public class ChangeNavigationModeUseCaseHandler : UseCaseHandlerBase<ChangeNavigationModeUseCaseRequest>
    {
        public ChangeNavigationModeUseCaseHandler(IDbContext db) : base(db) { }

        public async override Task<Unit> Handle(ChangeNavigationModeUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeNavigationMode(request.NavigationMode);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
