using System;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Groups.CreateGroup
{
    public class CreateGroupUseCaseHandler : UseCaseHandlerBase<CreateGroupUseCaseRequest, Guid>
    {
        private readonly IGroupUniquenessChecker _uniquenessChecker;
        public CreateGroupUseCaseHandler(AlphaTestContext db, IGroupUniquenessChecker uniquenessChecker) : base(db)
        {
            _uniquenessChecker = uniquenessChecker;
        }

        public override Task<Guid> Handle(CreateGroupUseCaseRequest request, CancellationToken cancellationToken)
        {
            bool groupAlreadyExists = _uniquenessChecker.CheckIfGroupExists(request.Name, request.BeginDate, request.EndDate);
            Group group = new(request.Name, request.BeginDate, request.EndDate, groupAlreadyExists);
            _db.Groups.Add(group);
            _db.SaveChanges();
            return Task.FromResult(group.ID);
        }
    }
}
