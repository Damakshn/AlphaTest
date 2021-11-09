using AlphaTest.Application;
using AlphaTest.Application.UseCases.Examinations.Commands.StartWork;
using AlphaTest.WebApi.Models.Examination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public ExaminationController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("{examinationID}/start")]
        public async Task<IActionResult> StartTesting([FromRoute] Guid examinationID)
        {
            // ToDo auth
            Guid dummyUserID = Guid.NewGuid();
            await _alphaTest.ExecuteUseCaseAsync(new StartWorkUseCaseRequest(dummyUserID, examinationID));
            return Ok();
        }

        [HttpPost("{examinationID}/questions/{questionID}/answer")]
        public IActionResult AcceptAnswer([FromRoute] Guid examinationID, [FromRoute] Guid questionID, [FromBody] AcceptAnswerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
