using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AssignProposal
{
    public class AssignProposalUseCaseHandler : UseCaseHandlerBase<AssignProposalUseCaseRequest>
    {
        public AssignProposalUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AssignProposalUseCaseRequest request, CancellationToken cancellationToken)
        {
            PublishingProposal proposal = await _db.PublishingProposals.Aggregates().FindByID(request.ProposalID);
            AlphaTestUser assignee = await _db.Users.Aggregates().FindByID(request.AssigneeID);
            proposal.AssignTo(assignee);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
