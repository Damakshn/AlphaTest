using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
using AlphaTest.Application.UseCases.Tests.Commands.AddQuestion;
using AlphaTest.Application.UseCases.Tests.Commands.QuestionList;
using AlphaTest.Application.UseCases.Tests.Commands.ChangeScoreDistribution;
using AlphaTest.Application.UseCases.Tests.Commands.AddContributor;
using AlphaTest.Application.UseCases.Tests.Commands.RemoveContributor;
using AlphaTest.Application.UseCases.Tests.Commands.SwitchAuthor;
using AlphaTest.Application.UseCases.Tests.Commands.EditQuestion;

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

        [HttpPost("{testID}/scoreDistribution")]
        public async Task<IActionResult> ConfigureScoreDistribution([FromRoute] Guid testID, [FromBody] ConfigureScoreDistributionRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new ChangeScoreDistributionUseCaseRequest(testID,
                request.ScoreDistributionMethodID,
                request.Score));
            return Ok();
        }
        #endregion

        #region Вопросы
        [HttpPost("{testID}/questions")]
        public async Task<IActionResult> AddQuestion([FromRoute] Guid testID, [FromBody] AddQuestionRequest request)
        {   
            // ToDo валидация
            //  для вопросов с точным ответом должен быть указан верный ответ, если он не пришёл, то это должно считаться ошибкой
            // ToDo может ещё как-то улучшить
            // ToDo кортеж почему-то не десериализуется из json, временно закостылено
            AddQuestionUseCaseRequest useCaseRequest = request.QuestionType switch
            {
                "SingleChoiceQuestion" => new AddSingleChoiceQuestionUseCaseRequest(testID, request.Text, request.Score, request.Options.Select(d => (text: d.Text, number: d.Number, isRight: d.IsRight)).ToList()),
                "MultiChoiceQuestion" => new AddMultichoiceQuestionUseCaseRequest(testID, request.Text, request.Score, request.Options.Select(d => (text: d.Text, number: d.Number, isRight: d.IsRight)).ToList()),
                "QuestionWithTextualAnswer" => new AddQuestionWithTextualAnswerUseCaseRequest(testID, request.Text, request.Score, request.TextualAnswer),
                "QuestionWithNumericAnswer" => new AddQuestionWithNumericAnswerUseCaseRequest(testID, request.Text, request.Score, request.NumericAnswer),
                "QuestionWithDetailedAnswer" => new AddQuestionWithDetailedAnswerUseCaseRequest(testID, request.Text, request.Score),
                _ => throw new ArgumentException("Не указан тип вопроса для добавления.")
            };
            Guid questionID = await _alphaTest.ExecuteUseCaseAsync(useCaseRequest);
            // ToDo вернуть url
            return Content(questionID.ToString());
        }

        [HttpDelete("{testID}/questions/{questionID}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid testID, [FromRoute] Guid questionID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new DeleteQuestionUseCaseRequest(testID, questionID));
            return Ok();
        }

        [HttpPatch("{testID}/questions/{questionID}")]
        public async Task<IActionResult> EditQuestion([FromRoute] Guid testID, [FromRoute] Guid questionID, [FromBody] EditQuestionRequest request)
        {
            // ToDo валидация
            // ToDo может ещё как-то улучшить
            // ToDo кортеж почему-то не десериализуется из json, временно закостылено
            EditQuestionUseCaseRequest useCaseRequest = request.QuestionType switch
            {
                var qt when
                    qt == "SingleChoiceQuestion" ||
                    qt == "MultiChoiceQuestion" ||
                    qt == "QuestionWithChoices" => new EditQuestionWithChoicesUseCaseRequest(
                        testID, 
                        questionID, 
                        request.Score, 
                        request.Text, 
                        request.Options is not null 
                            ? request.Options.Select(d => (text: d.Text, number: d.Number, isRight: d.IsRight)).ToList()
                            : null),
                "QuestionWithTextualAnswer" => new EditQuestionWithTextualAnswerUseCaseRequest(testID, questionID, request.Score, request.Text, request.TextualAnswer),
                "QuestionWithNumericAnswer" => new EditQuestionWithNumericAnswerUseCaseRequest(testID, questionID, request.Score, request.Text, request.NumericAnswer),
                "QuestionWithDetailedAnswer" => new EditQuestionWithDetailedAnswerUseCaseRequest(testID, questionID, request.Score, request.Text),
                _ => throw new ArgumentException("Не указан тип вопроса для редактирования.")
            };
            await _alphaTest.ExecuteUseCaseAsync(useCaseRequest);
            return Ok();
        }

        [HttpPost("{testID}/questions/reorder")]
        public async Task<IActionResult> ReorderQuestions([FromRoute] Guid testID, [FromBody] ReorderQuestionsRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new MoveQuestionsUseCaseRequest(
                    testID,
                    request.QuestionOrder.Select(x => (x.QuestionID, x.Position)).ToList()
                ));
            // MAYBE return url
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

        #region Составители
        [HttpPost("{testID}/contributors")]
        public async Task<IActionResult> AddContributor([FromRoute] Guid testID, [FromBody] AddContributorRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new AddContributorUseCaseRequest(testID, request.TeacherID));
            return Ok();
        }

        [HttpDelete("{testID}/contributors/{teacherID}")]
        public async Task<IActionResult> RemoveContributor([FromRoute] Guid testID, [FromRoute] Guid teacherID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new RemoveContributorUseCaseRequest(testID, teacherID));
            return Ok();
        }

        [HttpPost("{testID}/switchAuthor")]
        public async Task<IActionResult> SwitchAuthor([FromRoute] Guid testID, [FromBody] SwitchAuthorRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new SwitchAuthorUseCaseRequest(testID, request.NewAuthorID));
            return Ok();
        }
        #endregion
    }
}
