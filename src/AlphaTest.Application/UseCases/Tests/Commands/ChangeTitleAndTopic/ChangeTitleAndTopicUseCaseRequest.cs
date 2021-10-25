using System;
using MediatR;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTitleAndTopic
{
    public class ChangeTitleAndTopicUseCaseRequest : IUseCaseRequest
    {
        public Guid TestID { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }
    }
}
