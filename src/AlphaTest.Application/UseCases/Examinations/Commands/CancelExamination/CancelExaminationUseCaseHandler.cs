using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Examinations;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;

namespace AlphaTest.Application.UseCases.Examinations.Commands.CancelExamination
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
