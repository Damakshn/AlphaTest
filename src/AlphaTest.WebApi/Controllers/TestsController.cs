using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Tests.Commands.CreateTest;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo;

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

        [HttpGet("{testID}")]
        public async Task<TestInfo> ViewTestInfo(Guid testID)
        {
            var request = new ViewTestInfoQuery() { TestID = testID };
            return await _alphaTest.ExecuteUseCaseAsync(request);
        }
    }
}
