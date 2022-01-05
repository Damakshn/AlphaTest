using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Schedule.Commands.CreateExamination
{
    public class CreateExaminationUseCaseHandler : UseCaseHandlerBase<CreateExaminationUseCaseRequest, Guid>
    {
        public CreateExaminationUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Guid> Handle(CreateExaminationUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            AlphaTestUser examiner = await _db.Users.Aggregates().FindByID(request.UserID);

            List<Group> groups =
                request.Groups.Any()
                ? await _db.Groups.Aggregates().FindManyByIds(request.Groups)
                : new List<Group>();

            Examination examination = new(test, request.StartsAt, request.EndsAt, examiner, groups);
            _db.Examinations.Add(examination);
            _db.SaveChanges();
            return examination.ID;
        }
    }
}
