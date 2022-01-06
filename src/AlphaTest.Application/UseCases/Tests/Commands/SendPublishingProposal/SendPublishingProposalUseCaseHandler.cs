using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.SendPublishingProposal
{
    public class SendPublishingProposalUseCaseHandler : UseCaseHandlerBase<SendPublishingProposalUseCaseRequest, Guid>
    {
        public SendPublishingProposalUseCaseHandler(IDbContext db): base(db) { }

        public override async Task<Guid> Handle(SendPublishingProposalUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            List<Question> allQuestionInTest = await _db.Questions.Aggregates().FilterByTest(test.ID).ToListAsync();
            PublishingProposal proposal = test.ProposeForPublishing(allQuestionInTest);
            _db.PublishingProposals.Add(proposal);
            await _db.SaveChangesAsync(cancellationToken);
            return proposal.ID;
        }
    }
}

