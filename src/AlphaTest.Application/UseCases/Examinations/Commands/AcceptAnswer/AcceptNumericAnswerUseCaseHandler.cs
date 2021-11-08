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
    public class AcceptNumericAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<ExactNumericAnswer, QuestionWithNumericAnswer, AcceptNumericAnswerUseCaseRequest>
    {
        public AcceptNumericAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override ExactNumericAnswer MakeAnswer(AcceptNumericAnswerUseCaseRequest request, Work work, Test test, QuestionWithNumericAnswer question)
        {
            throw new NotImplementedException();
        }
    }
}
