using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptSingleChoiceAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<SingleChoiceAnswer, SingleChoiceQuestion, AcceptSingleChoiceAnswerUseCaseRequest>
    {
        public AcceptSingleChoiceAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override SingleChoiceAnswer MakeAnswer(
            AcceptSingleChoiceAnswerUseCaseRequest request,
            Work work,
            Test test,
            SingleChoiceQuestion question,
            uint answersAccepted)
        {
            return new SingleChoiceAnswer(question, work, test, answersAccepted, request.RightOptionID);
        }
    }
}
