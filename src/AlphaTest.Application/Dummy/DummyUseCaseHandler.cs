using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace AlphaTest.Application.Dummy
{
    public class DummyUseCaseHandler : IRequestHandler<DummyUseCaseRequest, string>
    {
        public Task<string> Handle(DummyUseCaseRequest request, CancellationToken cancellationToken)
        {
            var builder = new StringBuilder();
            builder.Append(request.RequestText);
            builder.Append("_");
            builder.Append(Guid.NewGuid().ToString());
            return Task.FromResult(builder.ToString());
        }
    }
}
