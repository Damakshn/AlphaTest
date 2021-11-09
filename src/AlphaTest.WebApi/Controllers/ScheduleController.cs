using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaTest.WebApi.Models.Admin.Examinations;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Schedule.Commands.CreateExamination;
using AlphaTest.Application.UseCases.Schedule.Commands.ChangeExaminationTerms;
using AlphaTest.Application.UseCases.Schedule.Commands.CancelExamination;
using AlphaTest.Application.UseCases.Admin.Commands.Examinations.SwitchExaminer;
using AlphaTest.WebApi.Models.Schedule;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]/examinations")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public ScheduleController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewExamination([FromBody] CreateExaminationRequest request)
        {
            // ToDo group list validation not null
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

        [HttpPut("{examinationID}/terms")]
        public async Task<IActionResult> ChangeExaminationTerms([FromRoute] Guid examinationID, [FromBody] ChangeExaminationTermsRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeExaminationTermsUseCaseRequest(examinationID, request.StartsAt, request.EndsAt));
            return Ok();
        }

        [HttpDelete("{examinationID}")]
        public async Task<IActionResult> CancelExamination([FromRoute] Guid examinationID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new CancelExaminationUseCaseRequest(examinationID));
            return Ok();
        }

        [HttpPut("{examinationID}/examiner")]
        public async Task<IActionResult> SwitchExaminer([FromRoute] Guid examinationID, [FromBody] SwitchExaminerRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new SwitchExaminerUseCaseRequest(examinationID, request.ExaminerID));
            return Ok();
        }
    }
}
