using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.RemoveContributor
{
    public class RemoveContributorUseCaseHandler : UseCaseHandlerBase<RemoveContributorUseCaseRequest>
    {
        public RemoveContributorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(RemoveContributorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.RemoveContributor(request.TeacherID);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
