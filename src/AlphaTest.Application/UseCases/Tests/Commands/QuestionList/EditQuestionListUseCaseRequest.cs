using AlphaTest.Application.UseCases.Common;
using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public abstract class EditQuestionListUseCaseRequest : IUseCaseRequest
    {
        public Guid TestID { get; protected set; }
    }
}
