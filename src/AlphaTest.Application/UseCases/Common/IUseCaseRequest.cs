using MediatR;

namespace AlphaTest.Application.UseCases.Common
{
    public interface IUseCaseRequest : IRequest
    {
    }

    public interface IUseCaseRequest<TResponse> : IRequest<TResponse>
    {
    }
}
