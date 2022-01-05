﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Schedule.Commands.ChangeExaminationTerms
{
    public class ChangeExaminationTermsUseCaseHandler : UseCaseHandlerBase<ChangeExaminationTermsUseCaseRequest>
    {
        public ChangeExaminationTermsUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(ChangeExaminationTermsUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            Test test = await _db.Tests.Aggregates().FindByID(examination.TestID);
            examination.ChangeDates(request.StartsAt, request.EndsAt, test);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
