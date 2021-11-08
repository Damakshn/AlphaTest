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
    public class AcceptTextualAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<ExactTextualAnswer, QuestionWithTextualAnswer, AcceptTextualAnswerUseCaseRequest>
    {
        public AcceptTextualAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override ExactTextualAnswer MakeAnswer(AcceptTextualAnswerUseCaseRequest request, Work work, Test test, QuestionWithTextualAnswer question)
        {
            throw new NotImplementedException();
        }
    }
}
