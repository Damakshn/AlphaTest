using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlphaTest.Application;
using AlphaTest.Application.Dummy;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public HomeController(ISystemGateway systemGateway)
        {
            _alphaTest = systemGateway;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("Hello World!");
        }

        [HttpGet("dummy")]
        public async Task<string> Dummy(string text)
        {
            var request = new DummyUseCaseRequest() { RequestText = text };
            // TBD why can omit <string>
            return await _alphaTest.ExecuteUseCaseAsync<string>(request);
        }
    }
}
