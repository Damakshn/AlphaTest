﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeRevokePolicy
{
    public class ChangeRevokePolicyUseCaseHandler : UseCaseHandlerBase<ChangeRevokePolicyUseCaseRequest>
    {
        public ChangeRevokePolicyUseCaseHandler(AlphaTestContext db): base(db) { }

        public async override Task<Unit> Handle(ChangeRevokePolicyUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeRevokePolicy(request.RevokePolicy);
            return Unit.Value;
        }
    }
}
