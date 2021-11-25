using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AssignProposal
{
    public class AssignProposalUseCaseHandler : UseCaseHandlerBase<AssignProposalUseCaseRequest>
    {
        public AssignProposalUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AssignProposalUseCaseRequest request, CancellationToken cancellationToken)
        {
            PublishingProposal proposal = await _db.PublishingProposals.Aggregates().FindByID(request.ProposalID);
            AppUser assignee = await _db.Users.Aggregates().FindByID(request.AssigneeID);
            proposal.AssignTo(assignee);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
