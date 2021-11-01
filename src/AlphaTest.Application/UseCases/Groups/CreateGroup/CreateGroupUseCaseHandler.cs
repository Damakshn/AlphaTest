using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Groups.CreateGroup
{
    public class CreateGroupUseCaseHandler : UseCaseHandlerBase<CreateGroupUseCaseRequest, Guid>
    {
        public CreateGroupUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override Task<Guid> Handle(CreateGroupUseCaseRequest request, CancellationToken cancellationToken)
        {
            bool groupAlreadyExists = _db.Groups.Any(g => g.Name == request.Name && (g.BeginDate >= request.BeginDate || g.EndDate <= request.EndDate));
            Group group = new(request.Name, request.BeginDate, request.EndDate, groupAlreadyExists);
            _db.Groups.Add(group);
            _db.SaveChanges();
            return Task.FromResult(group.ID);
        }
    }
}
