using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AlphaTest.Application;
using AlphaTest.Application.Models.Tests;
using AlphaTest.Application.Models.Questions;
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
using AlphaTest.Application.UseCases.Tests.Commands.NewEdition;
using AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionsList;
using AlphaTest.Application.UseCases.Tests.Queries.ViewQuestionInfo;
using AlphaTest.Application.UseCases.Tests.Queries.TestsList;
using AlphaTest.WebApi.Models.Tests.AddQuestion;
using AlphaTest.WebApi.Utils.Security;
using AlphaTest.WebApi.Models.Tests;

namespace AlphaTest.WebApi.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody]CreateTestRequest request)
        {
            var testID =  await _alphaTest.ExecuteUseCaseAsync(new CreateTestUseCaseRequest(request.Title, request.Topic, User.GetID()));
            // ToDo return url
            return Content(testID.ToString());
             
        }

        [Authorize(Policy = "AuthorOnly")]
        [HttpPost("{testID}/newEdition")]
        public async Task<IActionResult> CreateNewEdition([FromRoute] Guid testID)
        {
            Guid newEditionID = await _alphaTest.ExecuteUseCaseAsync(new CreateNewEditionOfTestUseCaseRequest(testID));
            return Ok(newEditionID);
        }
        #endregion

        #region Просмотр информации
        [Authorize(Policy = "CanViewTestContents")]
        [HttpGet("{testID}")]
        public async Task<TestInfo> ViewTestInfo(Guid testID)
        {
            var request = new ViewTestInfoQuery() { TestID = testID };
            return await _alphaTest.ExecuteUseCaseAsync(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<List<TestsListItemDto>> ViewFullTestsList(
            string title, 
            string topic, 
            Guid? authorID, 
            Guid? authorOrContributorID,
            [FromQuery]List<int> statuses,
            int pageSize = 20,
            int pageNumber = 1)
        {
            TestsListQuery query = new(title, topic, authorID, authorOrContributorID, statuses, pageSize, pageNumber);
            return await _alphaTest.ExecuteUseCaseAsync(query);
        }

        [Authorize(Policy = "CanViewTestContents")]
        [HttpGet("{testID}/questions")]
        public async Task<List<QuestionListItemDto>> ViewQuestionList([FromRoute] Guid testID)
        {
            ViewQuestionsListQuery request = new(testID);
            List<QuestionListItemDto> questionsInTest = await _alphaTest.ExecuteUseCaseAsync(request);
            return questionsInTest;
        }

        [Authorize(Policy = "CanViewTestContents")]
        [HttpGet("{testID}/questions/{questionID}")]
        public async Task<QuestionInfoDto> ViewSingleQuestion([FromRoute] Guid testID, [FromRoute] Guid questionID)
        {
            ViewQuestionInfoQuery query = new(testID, questionID);
            return await _alphaTest.ExecuteUseCaseAsync(query);
        }
        #endregion

        #region Редактирование
        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/rename")]
        public async Task<IActionResult> Rename([FromRoute] Guid testID, [FromBody] ChangeTitleAndTopicRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new ChangeTitleAndTopicUseCaseRequest() { TestID = testID, Title = request.Title, Topic = request.Topic });
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}")]
        public async Task<IActionResult> ChangeNavigationMode([FromRoute] Guid testID, [FromBody] ChangeNavigationModeRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeNavigationModeUseCaseRequest(testID, request.NavigationModeID));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/revokePolicy")]
        public async Task<IActionResult> ChangeRevokePolicy([FromRoute] Guid testID, [FromBody] ChangeRevokePolicyRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new ChangeRevokePolicyUseCaseRequest(testID, request.RevokeEnabled, request.RetriesLimit, request.InfiniteRetriesEnabled));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/timeLimit")]
        public async Task<IActionResult> ChangeTimeLimit([FromRoute] Guid testID, [FromBody] ChangeTimeLimitRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeTimeLimitUseCaseRequest(testID, request.TimeLimit));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/attemptsLimit")]
        public async Task<IActionResult> ChangeAttemptsLimit([FromRoute]Guid testID, ChangeAttemptsLimitRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeAttemptsLimitUseCaseRequest(testID, request.AttemptsLimit));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/checkingPolicy")]
        public async Task<IActionResult> ChangeCheckingPolicy([FromRoute] Guid testID, [FromBody] ChangeCheckingPolicyRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeCheckingPolicyUseCaseRequest(testID, request.PolicyID));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/passingScore")]
        public async Task<IActionResult> ChangePassingScore([FromRoute] Guid testID, [FromBody] ChangePassingScoreRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangePassingScoreUseCaseRequest(testID, request.NewScore));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/workCheckingMethod")]
        public async Task<IActionResult> ChangeWorkCheckingMethod([FromRoute] Guid testID, [FromBody] ChangeWorkCheckingMethodRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ChangeWorkCheckingMethodUseCaseRequest(testID, request.WorkCheckingMethodID));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
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
        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpPost("{testID}/questions")]
        public async Task<IActionResult> AddQuestion([FromRoute] Guid testID, [FromBody] AddQuestionRequest request)
        {
            // ToDo валидация
            //  для вопросов с точным ответом должен быть указан верный ответ, если он не пришёл, то это должно считаться ошибкой
            // ToDo может ещё как-то улучшить
            // ToDo кортеж не десериализуется из json, временно закостылено
            AddQuestionUseCaseRequest useCaseRequest = request switch
            {
                AddSingleChoiceQuestionRequest addSingleChoiceQuestionRequest =>
                    new AddSingleChoiceQuestionUseCaseRequest(
                        testID,
                        request.Text,
                        request.Score,
                        addSingleChoiceQuestionRequest.Options.Select(d => (text: d.Text, number: d.Number, isRight: d.IsRight)).ToList()),
                AddMultiChoiceQuestionRequest addMultiChoiceQuestionRequest =>
                    new AddMultichoiceQuestionUseCaseRequest(
                        testID,
                        request.Text,
                        request.Score,
                        addMultiChoiceQuestionRequest.Options.Select(d => (text: d.Text, number: d.Number, isRight: d.IsRight)).ToList()),
                AddQuestionWithTextualAnswerRequest addQuestionWithTextualAnswerRequest =>
                    new AddQuestionWithTextualAnswerUseCaseRequest(
                        testID,
                        request.Text,
                        request.Score,
                        addQuestionWithTextualAnswerRequest.RightAnswer),
                AddQuestionWithNumericAnswerRequest addQuestionWithNumericAnswerRequest =>
                    new AddQuestionWithNumericAnswerUseCaseRequest(
                        testID,
                        request.Text,
                        request.Score,
                        addQuestionWithNumericAnswerRequest.RightAnswer),
                AddQuestionWithDetailedAnswerRequest addQuestionWithDetailedAnswerRequest =>
                    new AddQuestionWithDetailedAnswerUseCaseRequest(
                        testID, 
                        request.Text, 
                        request.Score),
                _ => null
            };
            if (useCaseRequest is null)
                return BadRequest();
            Guid questionID = await _alphaTest.ExecuteUseCaseAsync(useCaseRequest);
            // ToDo вернуть url
            return Ok(questionID);

        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
        [HttpDelete("{testID}/questions/{questionID}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] Guid testID, [FromRoute] Guid questionID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new DeleteQuestionUseCaseRequest(testID, questionID));
            return Ok();
        }

        [Authorize(Policy = "AuthorOrContributorsOnly")]
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

        [Authorize(Policy = "AuthorOrContributorsOnly")]
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
        [Authorize(Policy = "AuthorOnly")]
        [HttpPost("{testID}/proposeForPublishing")]
        public async Task<IActionResult> ProposeTestForPublishing([FromRoute] Guid testID)
        {
            Guid proposalID = await _alphaTest.ExecuteUseCaseAsync(new SendPublishingProposalUseCaseRequest(testID));
            // ToDo return url
            return Ok(proposalID);
        }
        #endregion

        #region Составители
        [Authorize(Policy = "AuthorOnly")]
        [HttpPost("{testID}/contributors")]
        public async Task<IActionResult> AddContributor([FromRoute] Guid testID, [FromBody] AddContributorRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new AddContributorUseCaseRequest(testID, request.TeacherID));
            return Ok();
        }

        [Authorize(Policy = "AuthorOnly")]
        [HttpDelete("{testID}/contributors/{teacherID}")]
        public async Task<IActionResult> RemoveContributor([FromRoute] Guid testID, [FromRoute] Guid teacherID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new RemoveContributorUseCaseRequest(testID, teacherID));
            return Ok();
        }

        [Authorize(Policy = "CanSwitchAuthor")]
        [HttpPost("{testID}/switchAuthor")]
        public async Task<IActionResult> SwitchAuthor([FromRoute] Guid testID, [FromBody] SwitchAuthorRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new SwitchAuthorUseCaseRequest(testID, request.NewAuthorID));
            return Ok();
        }
        #endregion
    }
}
