using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Application.DataAccess.EF.Abstractions;


namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptTextualAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<ExactTextualAnswer, QuestionWithTextualAnswer, AcceptTextualAnswerUseCaseRequest>
    {
        public AcceptTextualAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override ExactTextualAnswer MakeAnswer(
            AcceptTextualAnswerUseCaseRequest request,
            Work work,
            Test test,
            QuestionWithTextualAnswer question,
            uint answersAccepted)
        {
            return new ExactTextualAnswer(question, work, test, answersAccepted, request.TextualAnswer);
        }
    }
}
