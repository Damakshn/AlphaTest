using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Auth.Commands.Login
{
    public class LoginUseCaseRequest : IUseCaseRequest<string>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
