using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaTest.WebApi.Models.Tests;
using AlphaTest.Application;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.UseCases.Tests.Commands.CreateTest;
using AlphaTest.Application.UseCases.Tests.Queries.ViewTestInfo;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeTitleAndTopic;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeNavigationMode;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeRevokePolicy;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeTimeLimit;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeAttemptsLimit;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeCheckingPolicy;
using AlphaTest.Application.UseCases.Tests.Commands.ChangePassingScore;
using AlphaTest.Application.UseCases.Tests.Commands.SendPublishingProposal;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeWorkCheckingMethod;

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

        #region Создание тестов
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]CreateTestUseCaseRequest request)
        {
            var testID =  await _alphaTest.ExecuteUseCaseAsync<Guid>(request);
            // ToDo return url
            return Content(testID.ToString());
             
        }
        #endregion

        #region Просмотр информации
        [HttpGet("{testID}")]
        public async Task<TestInfo> ViewTestInfo(Guid testID)
        {
            var request = new ViewTestInfoQuery() { TestID = testID };
            return await _alphaTest.ExecuteUseCaseAsync(request);
        }
        #endregion

        #region Редактирование
        [HttpPost("{testID}/rename")]
        public async Task<IActionResult> Rename([FromRoute] Guid testID, [FromBody] ChangeTitleAndTopicRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new ChangeTitleAndTopicUseCaseRequest() { TestID = testID, Title = request.Title, Topic = request.Topic });
            return Ok();
        }

        [HttpPost("{testID}")]
        public async Task<IActionResult> ChangeNavigationMode([FromRoute] Guid testID, [FromBody] ChangeNavigationModeRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeNavigationModeUseCaseRequest(testID, request.NavigationModeID));
            return Ok();
        }

        [HttpPost("{testID}/revokePolicy")]
        public async Task<IActionResult> ChangeRevokePolicy([FromRoute] Guid testID, [FromBody] ChangeRevokePolicyRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new ChangeRevokePolicyUseCaseRequest(testID, request.RevokeEnabled, request.RetriesLimit, request.InfiniteRetriesEnabled));
            return Ok();
        }

        [HttpPost("{testID}/timeLimit")]
        public async Task<IActionResult> ChangeTimeLimit([FromRoute] Guid testID, [FromBody] ChangeTimeLimitRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeTimeLimitUseCaseRequest(testID, request.TimeLimit));
            return Ok();
        }

        [HttpPost("{testID}/attemptsLimit")]
        public async Task<IActionResult> ChangeAttemptsLimit([FromRoute]Guid testID, ChangeAttemptsLimitRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeAttemptsLimitUseCaseRequest(testID, request.AttemptsLimit));
            return Ok();
        }

        [HttpPost("{testID}/checkingPolicy")]
        public async Task<IActionResult> ChangeCheckingPolicy([FromRoute] Guid testID, [FromBody] ChangeCheckingPolicyRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeCheckingPolicyUseCaseRequest(testID, request.PolicyID));
            return Ok();
        }

        [HttpPost("{testID}/passingScore")]
        public async Task<IActionResult> ChangePassingScore([FromRoute] Guid testID, [FromBody] ChangePassingScoreRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangePassingScoreUseCaseRequest(testID, request.NewScore));
            return Ok();
        }

        [HttpPost("{testID}/workCheckingMethod")]
        public async Task<IActionResult> ChangeWorkCheckingMethod([FromRoute] Guid testID, [FromBody] ChangeWorkCheckingMethodRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeWorkCheckingMethodUseCaseRequest(testID, request.WorkCheckingMethodID));
            return Ok();
        }
        #endregion

        #region Заявки
        [HttpPost("{testID}/proposeForPublishing")]
        public async Task<IActionResult> ProposeTestForPublishing([FromRoute] Guid testID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new SendPublishingProposalUseCaseRequest(testID));
            // ToDo return url
            return Ok();
        }
        #endregion
    }
}
