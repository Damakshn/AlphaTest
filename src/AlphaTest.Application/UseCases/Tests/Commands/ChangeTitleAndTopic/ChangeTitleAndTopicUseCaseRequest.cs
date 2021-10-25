using System;
using MediatR;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTitleAndTopic
{
    public class ChangeTitleAndTopicUseCaseRequest : IRequest
    {
        public Guid TestID { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }
    }
}
