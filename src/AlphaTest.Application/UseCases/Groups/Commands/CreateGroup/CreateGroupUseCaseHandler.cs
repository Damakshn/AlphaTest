using System;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.CreateGroup
{
    public class CreateGroupUseCaseHandler : UseCaseHandlerBase<CreateGroupUseCaseRequest, Guid>
    {
        private readonly IGroupUniquenessChecker _uniquenessChecker;
        public CreateGroupUseCaseHandler(IDbContext db, IGroupUniquenessChecker uniquenessChecker) : base(db)
        {
            _uniquenessChecker = uniquenessChecker;
        }

        public override async Task<Guid> Handle(CreateGroupUseCaseRequest request, CancellationToken cancellationToken)
        {
            bool groupAlreadyExists = _uniquenessChecker.CheckIfGroupExists(request.Name, request.BeginDate, request.EndDate);
            AlphaTestUser curator =
                request.CuratorID is null
                ? null
                : await _db.Users.Aggregates().FindByID((Guid)request.CuratorID);
            Group group = new(request.Name, request.BeginDate, request.EndDate, curator, groupAlreadyExists);
            _db.Groups.Add(group);
            await _db.SaveChangesAsync(cancellationToken);
            return group.ID;
        }
    }
}
