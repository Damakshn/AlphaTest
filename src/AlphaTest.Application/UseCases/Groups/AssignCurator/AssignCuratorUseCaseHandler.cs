﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Groups.AssignCurator
{
    public class AssignCuratorUseCaseHandler : UseCaseHandlerBase<AssignCuratorUseCaseRequest>
    {
        public AssignCuratorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AssignCuratorUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser curator = await _db.Users.Aggregates().FindByID(request.CuratorID);
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            group.AssignCurator(curator);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
