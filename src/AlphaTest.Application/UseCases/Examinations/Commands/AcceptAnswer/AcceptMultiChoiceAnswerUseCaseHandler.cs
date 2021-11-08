﻿using AlphaTest.Core.Answers;
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
    public class AcceptMultiChoiceAnswerUseCaseHandler : AcceptAnswerUseCaseHandler<MultiChoiceAnswer, MultiChoiceQuestion, AcceptMultiChoiceAnswerUseCaseRequest>
    {
        public AcceptMultiChoiceAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override MultiChoiceAnswer MakeAnswer(AcceptMultiChoiceAnswerUseCaseRequest request, Work work, Test test, MultiChoiceQuestion question)
        {
            throw new NotImplementedException();
        }
    }
}