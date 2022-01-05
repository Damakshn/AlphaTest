using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Checking.CheckAnswerManually
{
    public class CheckAnswerManuallyUseCaseHandler : UseCaseHandlerBase<CheckAnswerManuallyUseCaseRequest>
    {
        public CheckAnswerManuallyUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(CheckAnswerManuallyUseCaseRequest request, CancellationToken cancellationToken)
        {
            // ToDo определить, что должно произойти, если ответ на вопрос уже был оценён (тем же преподавателем, или другим)
            Answer answerToCheck = await _db.Answers.Aggregates().FindByID(request.AnswerID);
            Question questionOfAnswer = await _db.Questions.Aggregates().FindByID(answerToCheck.QuestionID);
            PreliminaryResult preliminaryResult = new(questionOfAnswer, answerToCheck, request.Score, request.CheckResultType);
            Test test = await _db.Tests.Aggregates().FindByID(questionOfAnswer.TestID);
            AdjustedResult adjustedResult = preliminaryResult.AdjustWithCheckingPolicy(test.CheckingPolicy);
            CheckResult finalResult = new(adjustedResult, request.TeacherID);
            _db.Results.Add(finalResult);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
