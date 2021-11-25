using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Profile.ChangePassword;
using AlphaTest.WebApi.Models.Profile;

namespace AlphaTest.WebApi.Controllers
{
    // Todo access control profile owner only
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public ProfileController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("{userID}/changePassword")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid userID, [FromBody] ChangePasswordRequest request)
        {   
            await _alphaTest.ExecuteUseCaseAsync(new ChangePasswordUseCaseRequest(userID, request.OldPassword, request.NewPassword, request.NewPasswordRepeat));
            return Ok();
        }
    }
}
