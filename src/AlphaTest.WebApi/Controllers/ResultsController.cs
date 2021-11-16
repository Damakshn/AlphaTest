using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Checking.CheckAnswerManually;
using AlphaTest.WebApi.Models.Results;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ISystemGateway _alphaTest;

        public ResultsController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost]
        public async Task<IActionResult> CheckAnswerManually([FromBody] CheckResultManuallyRequest request)
        {
            // ToDo auth
            Guid teacherID = Guid.NewGuid();
            await _alphaTest.ExecuteUseCaseAsync(
                new CheckAnswerManuallyUseCaseRequest(
                    request.AnswerID,
                    teacherID,
                    request.Score,
                    request.CheckResultTypeID));
            return Ok();
        }
    }
}
