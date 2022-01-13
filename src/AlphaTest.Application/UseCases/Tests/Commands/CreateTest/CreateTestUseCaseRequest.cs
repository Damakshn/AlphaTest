using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseRequest : IUseCaseRequest<Guid>
    {
        public CreateTestUseCaseRequest(string title, string topic, Guid authorID)
        {
            Title = title;
            Topic = topic;
            AuthorID = authorID;
        }

        public string Title { get; private set; }

        public string Topic { get; private set; }

        public Guid AuthorID { get; private set; }
    }
}
