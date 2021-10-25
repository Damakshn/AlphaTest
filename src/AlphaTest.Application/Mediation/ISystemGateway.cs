using MediatR;
using System.Threading.Tasks;

namespace AlphaTest.Application
{
    public interface ISystemGateway
    {
        Task<TResponse> ExecuteUseCaseAsync<TResponse>(IRequest<TResponse> request);

        Task ExecuteUseCaseAsync(IRequest request);
    }
}
