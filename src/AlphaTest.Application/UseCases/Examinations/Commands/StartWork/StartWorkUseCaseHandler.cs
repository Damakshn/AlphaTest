using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Works;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.StartWork
{
    public class StartWorkUseCaseHandler : UseCaseHandlerBase<StartWorkUseCaseRequest>
    {
        public StartWorkUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(StartWorkUseCaseRequest request, CancellationToken cancellationToken)
        {
            Examination examination = await _db.Examinations.Aggregates().FindByID(request.ExaminationID);
            Test test = await _db.Tests.Aggregates().FindByID(examination.TestID);
            uint attemptsSpent = (uint)await _db.Works.CountAsync(w => w.ExaminationID == examination.ID && w.StudentID == request.StudentID);

            #region Закостылено, ToDo аутентификация
            AppUser dummyStudent = await _db.Users.Aggregates().FindByUsername("dummystudent@mail.ru");
            Work work = new(test, examination, /*request.StudentID*/dummyStudent.Id, attemptsSpent);
            #endregion

            _db.Works.Add(work);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
