using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaTest.WebApi.Models.Examinations;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Examinations.Commands.CreateExamination;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationsController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public ExaminationsController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewExamination([FromBody] CreateExaminationRequest request)
        {
            Guid examID = await _alphaTest.ExecuteUseCaseAsync(
                new CreateExaminationUseCaseRequest(
                    request.TestID, 
                    request.StartsAt,
                    request.EndsAt, 
                    Guid.NewGuid(), // ToDo current user
                    request.Groups));
            // todo return url
            return Ok(examID);
        }
    }
}
