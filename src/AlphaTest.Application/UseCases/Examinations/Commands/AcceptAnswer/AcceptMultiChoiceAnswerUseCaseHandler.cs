using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptMultiChoiceAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<MultiChoiceAnswer, MultiChoiceQuestion, AcceptMultiChoiceAnswerUseCaseRequest>
    {
        public AcceptMultiChoiceAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override MultiChoiceAnswer MakeAnswer(
            AcceptMultiChoiceAnswerUseCaseRequest request,
            Work work,
            Test test,
            MultiChoiceQuestion question,
            uint answersAccepted)
        {
            return new MultiChoiceAnswer(question, work, test, answersAccepted, request.RightOptions);
        }
    }
}
