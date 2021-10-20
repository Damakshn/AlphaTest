using MediatR;

namespace AlphaTest.Application.Dummy
{
    public class DummyUseCaseRequest : IRequest<string>
    {
        public string RequestText { get; set; }
    }
}
