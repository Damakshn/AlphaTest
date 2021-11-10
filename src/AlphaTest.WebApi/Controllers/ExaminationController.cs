using AlphaTest.Application;
using AlphaTest.Application.UseCases.Examinations.Commands.StartWork;
using AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer;
using AlphaTest.WebApi.Models.Examination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Examinations.Commands.RevokeAnswer;

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
        public async Task<IActionResult> AcceptAnswer([FromRoute] Guid examinationID, [FromRoute] Guid questionID, [FromBody] AcceptAnswerRequest request)
        {
            Guid studentID = Guid.NewGuid();
            AcceptAnswerUseCaseRequest useCaseRequest = request switch
            {
                AcceptSingleChoiceAnswerRequest acceptSingleChoiceAnswerRequest =>
                    new AcceptSingleChoiceAnswerUseCaseRequest(
                        examinationID,
                        studentID,
                        questionID,
                        acceptSingleChoiceAnswerRequest.RightOptionID),
                AcceptMultiChoiceAnswerRequest acceptMultiChoiceAnswerRequest =>
                    new AcceptMultiChoiceAnswerUseCaseRequest(
                            examinationID,
                            studentID,
                            questionID,
                            acceptMultiChoiceAnswerRequest.RightOptions),
                AcceptNumericAnswerRequest acceptNumericAnswerRequest =>
                    new AcceptNumericAnswerUseCaseRequest(
                        examinationID, 
                        studentID, 
                        questionID, 
                        acceptNumericAnswerRequest.NumericAnswer),
                AcceptTextualAnswerRequest acceptTextualAnswerRequest =>
                    new AcceptTextualAnswerUseCaseRequest(
                        examinationID, 
                        studentID, 
                        questionID, 
                        acceptTextualAnswerRequest.TextualAnswer),
                AcceptDetailedAnswerRequest acceptDetailedAnswerRequest =>
                    new AcceptDetailedAnswerUseCaseRequest(
                        examinationID, 
                        studentID, 
                        questionID, 
                        acceptDetailedAnswerRequest.DetailedAnswer),
                _ => throw new InvalidOperationException("Некорректное содержание ответа.")
            };
            await _alphaTest.ExecuteUseCaseAsync(useCaseRequest);
            return Ok();
        }

        [HttpDelete("{examinationID}/questions/{questionID}/answer")]
        public async Task<IActionResult> RevokeAnswer([FromRoute] Guid examinationID, [FromRoute] Guid questionID)
        {
            // ToDo auth
            Guid studentID = Guid.NewGuid();
            await _alphaTest.ExecuteUseCaseAsync(new RevokeAnswerUseCaseRequest(examinationID, questionID, studentID));
            return Ok();
        }
    }
}
