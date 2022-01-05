using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.SwitchAuthor
{
    public class SwitchAuthorUseCaseHandler : UseCaseHandlerBase<SwitchAuthorUseCaseRequest>
    {
        public SwitchAuthorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(SwitchAuthorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            AlphaTestUser user = await _db.Users.Aggregates().FindByID(request.NewAuthorID);
            test.SwitchAuthor(user);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
