using AlphaTest.Application;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Auth.Commands.Login;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlphaTest.WebApi.Controllers
{   
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public AuthController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUseCaseRequest request)
        {
            try
            {
                string token = await _alphaTest.ExecuteUseCaseAsync(request);
                return new JsonResult(new { Token = token });
            }
            catch (AlphaTestApplicationException applicationException)
            {   
                return Unauthorized(applicationException.Message);
            }
        }
    }
}
