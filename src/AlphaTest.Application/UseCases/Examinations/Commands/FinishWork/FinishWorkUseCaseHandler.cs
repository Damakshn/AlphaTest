using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Works;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
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
            Work workToFinish = await _db.Works.Aggregates().GetActiveWork(currentExamination.ID, request.StudentID);
            workToFinish.ManualFinish();
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
