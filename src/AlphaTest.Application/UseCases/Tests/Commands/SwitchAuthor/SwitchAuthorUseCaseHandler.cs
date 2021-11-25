using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;

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
            AppUser user = await _db.Users.Aggregates().FindByID(request.NewAuthorID);
            test.SwitchAuthor(user);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
