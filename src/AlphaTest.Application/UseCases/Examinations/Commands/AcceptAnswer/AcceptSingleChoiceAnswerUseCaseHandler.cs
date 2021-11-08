using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Works;
using AlphaTest.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptSingleChoiceAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<SingleChoiceAnswer, SingleChoiceQuestion, AcceptSingleChoiceAnswerUseCaseRequest>
    {
        public AcceptSingleChoiceAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override SingleChoiceAnswer MakeAnswer(AcceptSingleChoiceAnswerUseCaseRequest request, Work work, Test test, SingleChoiceQuestion question)
        {
            throw new NotImplementedException();
        }
    }
}
