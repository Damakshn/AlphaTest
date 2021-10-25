using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Publishing;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Tests.Commands.SendPublishingProposal
{
    public class SendPublishingProposalUseCaseHandler : UseCaseHandlerBase<SendPublishingProposalUseCaseRequest, Guid>
    {
        public SendPublishingProposalUseCaseHandler(AlphaTestContext db): base(db) { }

        public override async Task<Guid> Handle(SendPublishingProposalUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            List<Question> allQuestionInTest = await _db.Questions.Aggregates().FilterByTest(test.ID).ToListAsync();
            PublishingProposal proposal = test.ProposeForPublishing(allQuestionInTest);
            _db.PublishingProposals.Add(proposal);
            _db.SaveChanges();
            return proposal.ID;
        }
    }
}

