using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using MediatR;

namespace AlphaTest.Application.UseCases.Admin.Commands.Examinations.SwitchExaminer
{
    public class SwitchExaminerUseCaseHandler : UseCaseHandlerBase<SwitchExaminerUseCaseRequest>
    {
        public SwitchExaminerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(SwitchExaminerUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            Test test = await _db.Tests.Aggregates().FindByID(examination.TestID);
            AlphaTestUser examiner = await _db.Users.Aggregates().FindByID(request.ExaminerID);
            examination.SwitchExaminer(examiner, test);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
