using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Core.Users;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth.JWT;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Auth.Commands.Login
{
    public class LoginUseCaseHandler : UseCaseHandlerBase<LoginUseCaseRequest, string>
    {
        private readonly UserManager<AlphaTestUser> _userManager;
        private readonly SignInManager<AlphaTestUser> _signInManager;
        private readonly JwtGenerator _jwtGenerator;


        public LoginUseCaseHandler(AlphaTestContext db, UserManager<AlphaTestUser> userManager, SignInManager<AlphaTestUser> signInManager, JwtGenerator jwtGenerator) : base(db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public override async Task<string> Handle(LoginUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new AlphaTestApplicationException($"Ошибка входа в систему - пользователь {request.Email} не найден.");
            }
            if (user.IsTemporaryPasswordExpired())
            {
                throw new AlphaTestApplicationException($"Ошибка входа в систему - ваш временный пароль просрочен. Обратитесь к администратору.");
            }
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signInResult.Succeeded)
            {
                throw new AlphaTestApplicationException($"Ошибка входа в систему - неправильный пароль.");
            }
            return await _jwtGenerator.GetTokenAsync(user);
        }
    }
}
