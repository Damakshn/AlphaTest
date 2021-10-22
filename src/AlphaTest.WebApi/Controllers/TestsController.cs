using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Tests.Commands.CreateTest;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public TestsController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]CreateTestUseCaseRequest request)
        {
            var testID =  await _alphaTest.ExecuteUseCaseAsync<Guid>(request);
            // ToDo return url
            return Content(testID.ToString());
             
        }
    }
}
