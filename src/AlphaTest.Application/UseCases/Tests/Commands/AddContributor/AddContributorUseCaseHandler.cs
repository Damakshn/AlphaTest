using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddContributor
{
    public class AddContributorUseCaseHandler : UseCaseHandlerBase<AddContributorUseCaseRequest>
    {
        public AddContributorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AddContributorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            AlphaTestUser teacher = await _db.Users.Aggregates().FindByID(request.TeacherID);
            test.AddContributor(teacher);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
