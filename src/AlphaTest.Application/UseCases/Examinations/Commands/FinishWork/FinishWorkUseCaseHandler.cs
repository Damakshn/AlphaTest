using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Works;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.FinishWork
{
    public class FinishWorkUseCaseHandler : UseCaseHandlerBase<FinishCurrentWorkUseCaseRequest>
    {
        public FinishWorkUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(FinishCurrentWorkUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination currentExamination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            Work workToFinish = await _db.Works.Aggregates().GetActiveWork(currentExamination.ID, request.StudentID);
            workToFinish.ManualFinish();
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
