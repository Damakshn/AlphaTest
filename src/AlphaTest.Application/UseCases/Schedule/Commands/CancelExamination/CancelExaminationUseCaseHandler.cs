using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Examinations;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Schedule.Commands.CancelExamination
{
    public class CancelExaminationUseCaseHandler : UseCaseHandlerBase<CancelExaminationUseCaseRequest>
    {
        public CancelExaminationUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(CancelExaminationUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            examination.Cancel();
            // ToDo stop current attempts
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
