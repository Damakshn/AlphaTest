﻿using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Works;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;

namespace AlphaTest.Application.UseCases.Examinations.Commands.FinishWork
{
    public class FinishWorkUseCaseHandler : UseCaseHandlerBase<FinishCurrentWorkUseCaseRequest>
    {
        public FinishWorkUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(FinishCurrentWorkUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination currentExamination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);

            // ToDo auth закостылено
            AppUser dummyStudent = await _db.Users.Aggregates().FindByUsername("dummystudent@mail.ru");

            Work workToFinish = await _db.Works.Aggregates().GetActiveWork(currentExamination.ID, dummyStudent.Id);
            workToFinish.ManualFinish();
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}