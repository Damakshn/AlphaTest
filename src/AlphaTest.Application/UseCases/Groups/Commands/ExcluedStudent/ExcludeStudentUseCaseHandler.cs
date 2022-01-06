using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.ExcluedStudent
{
    public class ExcludeStudentUseCaseHandler : UseCaseHandlerBase<ExcludeStudentUseCaseRequest>
    {
        public ExcludeStudentUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(ExcludeStudentUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            AlphaTestUser student = await _db.Users.Aggregates().FindByID(request.StudentID);
            group.ExcludeStudent(student);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
