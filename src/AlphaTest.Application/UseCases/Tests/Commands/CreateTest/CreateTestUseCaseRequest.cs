using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseRequest : IUseCaseRequest<Guid>
    {
        public string Title { get; set; }

        public string Topic { get; set; }

        public string Username { get; set; }
    }
}
