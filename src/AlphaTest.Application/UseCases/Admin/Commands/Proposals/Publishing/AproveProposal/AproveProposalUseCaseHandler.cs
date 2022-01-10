using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AproveProposal
{
    public class AproveProposalUseCaseHandler : UseCaseHandlerBase<AproveProposalUseCaseRequest>
    {
        public AproveProposalUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AproveProposalUseCaseRequest request, CancellationToken cancellationToken)
        {
            // ToDo обработать может только тот же пользователь, что взял заявку в работу
            PublishingProposal proposal = await _db.PublishingProposals.Aggregates().FindByID(request.ProposalID);
            proposal.Approve();
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
