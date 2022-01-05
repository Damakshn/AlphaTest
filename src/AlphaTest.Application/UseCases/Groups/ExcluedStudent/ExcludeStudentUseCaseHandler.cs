using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Groups.ExcluedStudent
{
    public class ExcludeStudentUseCaseHandler : UseCaseHandlerBase<ExcludeStudentUseCaseRequest>
    {
        public ExcludeStudentUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(ExcludeStudentUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            AlphaTestUser student = await _db.Users.Aggregates().FindByID(request.StudentID);
            group.ExcludeStudent(student);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
