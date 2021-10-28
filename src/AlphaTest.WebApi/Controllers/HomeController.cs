using Microsoft.AspNetCore.Mvc;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Welcome to AlphaTest!");
        }
    }
}
