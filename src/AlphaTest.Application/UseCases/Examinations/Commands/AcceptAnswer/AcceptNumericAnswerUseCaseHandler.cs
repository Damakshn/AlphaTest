using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Infrastructure.Database;


namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptNumericAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<ExactNumericAnswer, QuestionWithNumericAnswer, AcceptNumericAnswerUseCaseRequest>
    {
        public AcceptNumericAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override ExactNumericAnswer MakeAnswer(
            AcceptNumericAnswerUseCaseRequest request,
            Work work,
            Test test,
            QuestionWithNumericAnswer question,
            uint answersAccepted)
        {
            return new ExactNumericAnswer(question, work, test, answersAccepted, request.NumericAnswer);
        }
    }
}
