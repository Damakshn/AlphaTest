using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;

namespace AlphaTest.Application.UseCases.Groups.AddStudent
{
    public class AddStudentUseCaseHandler : UseCaseHandlerBase<AddStudentUseCaseRequest>
    {
        public AddStudentUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AddStudentUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            AppUser student = await _db.Users.Aggregates().FindByID(request.StudentID);
            group.AddStudent(student);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
