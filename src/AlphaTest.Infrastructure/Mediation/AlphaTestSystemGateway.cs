using System.Threading.Tasks;
using AlphaTest.Application;
using MediatR;

namespace AlphaTest.Infrastructure.Mediation
{
    public class AlphaTestSystemGateway : ISystemGateway
    {
        private IMediator _mediator;

        public AlphaTestSystemGateway(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> ExecuteUseCaseAsync<TResponse>(IRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
