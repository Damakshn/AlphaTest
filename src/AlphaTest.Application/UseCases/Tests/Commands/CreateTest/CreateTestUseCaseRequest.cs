using System;
using MediatR;

namespace AlphaTest.Application.UseCases.Tests.Commands.CreateTest
{
    public class CreateTestUseCaseRequest : IRequest<Guid>
    {
        public string Title { get; set; }

        public string Topic { get; set; }

        public string Username { get; set; }
    }
}
