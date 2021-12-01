using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth.JWT;
using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaTest.Application.UseCases.Auth.Commands.Login
{
    public class LoginUseCaseHandler : UseCaseHandlerBase<LoginUseCaseRequest, string>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtGenerator _jwtGenerator;


        public LoginUseCaseHandler(AlphaTestContext db, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtGenerator jwtGenerator) : base(db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public override async Task<string> Handle(LoginUseCaseRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
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
