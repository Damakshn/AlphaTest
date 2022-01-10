using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Works;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.StartWork
{
    public class StartWorkUseCaseHandler : UseCaseHandlerBase<StartWorkUseCaseRequest>
    {
        public StartWorkUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(StartWorkUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            Test test = await _db.Tests.Aggregates().FindByID(examination.TestID);
            uint attemptsSpent = (uint)await _db.Works.CountAsync(w => w.ExaminationID == examination.ID && w.StudentID == request.StudentID);
            Work work = new(test, examination, request.StudentID, attemptsSpent);
            _db.Works.Add(work);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
