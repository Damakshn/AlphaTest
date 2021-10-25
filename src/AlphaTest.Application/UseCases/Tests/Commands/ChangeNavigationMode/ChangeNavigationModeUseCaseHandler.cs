﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeNavigationMode
{
    public class ChangeNavigationModeUseCaseHandler : UseCaseHandlerBase<ChangeNavigationModeUseCaseRequest>
    {
        public ChangeNavigationModeUseCaseHandler(AlphaTestContext db) : base(db) { }

        public async override Task<Unit> Handle(ChangeNavigationModeUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeNavigationMode(request.NavigationMode);
            return Unit.Value;
        }
    }
}