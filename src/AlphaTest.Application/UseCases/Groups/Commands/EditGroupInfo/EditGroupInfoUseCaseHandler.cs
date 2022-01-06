using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.EditGroupInfo
{
    public class EditGroupInfoUseCaseHandler : UseCaseHandlerBase<EditGroupInfoUseCaseRequest>
    {
        private IGroupUniquenessChecker _uniquenessChecker;

        public EditGroupInfoUseCaseHandler(IDbContext db, IGroupUniquenessChecker uniquenessChecker) : base(db)
        {
            _uniquenessChecker = uniquenessChecker;
        }

        public override async Task<Unit> Handle(EditGroupInfoUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group groupToEdit = await _db.Groups.Aggregates().FindByID(request.GroupID);
            bool groupAlreadyExists = _uniquenessChecker.CheckIfOneMoreGroupExists(request.Name, request.BeginDate, request.EndDate, groupToEdit.ID);
            groupToEdit.Rename(request.Name, groupAlreadyExists);
            groupToEdit.ChangeDates(request.BeginDate, request.EndDate);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
