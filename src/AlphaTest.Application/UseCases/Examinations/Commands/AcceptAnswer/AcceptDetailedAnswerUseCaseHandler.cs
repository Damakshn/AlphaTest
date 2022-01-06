using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptDetailedAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<DetailedAnswer, QuestionWithDetailedAnswer, AcceptDetailedAnswerUseCaseRequest>
    {
        public AcceptDetailedAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override DetailedAnswer MakeAnswer(
            AcceptDetailedAnswerUseCaseRequest request,
            Work work,
            Test test,
            QuestionWithDetailedAnswer question,
            uint answersAccepted)
        {
            return new DetailedAnswer(question, work, test, answersAccepted, request.DetailedAnswer);
        }
    }
}
