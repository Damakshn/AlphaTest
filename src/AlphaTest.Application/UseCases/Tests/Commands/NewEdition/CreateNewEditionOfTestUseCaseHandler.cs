using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using Microsoft.EntityFrameworkCore;

namespace AlphaTest.Application.UseCases.Tests.Commands.NewEdition
{
    public class CreateNewEditionOfTestUseCaseHandler : UseCaseHandlerBase<CreateNewEditionOfTestUseCaseRequest, Guid>
    {
        public CreateNewEditionOfTestUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Guid> Handle(CreateNewEditionOfTestUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            List<Question> allQuestionsInTest = await _db.Questions.Aggregates().FilterByTest(request.TestID).ToListAsync();

            Test newEditionOfTest = test.Replicate();
            List<Question> replicatedQuestions = new();
            foreach (Question oldQuestion in allQuestionsInTest)
            {
                replicatedQuestions.Add(oldQuestion.ReplicateForNewEdition(newEditionOfTest));
            }
            _db.Tests.Add(newEditionOfTest);
            _db.Questions.AddRange(replicatedQuestions);
            await _db.SaveChangesAsync();
            return newEditionOfTest.ID;
        }
    }
}
