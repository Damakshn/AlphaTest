using System;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Groups.CreateGroup;
using AlphaTest.WebApi.Models.Groups;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Groups.DisbandGroup;
using AlphaTest.Application.UseCases.Groups.AddStudent;
using AlphaTest.Application.UseCases.Groups.ExcluedStudent;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public GroupsController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewGroup([FromBody] CreateGroupRequest request)
        {
            Guid groupID = await _alphaTest.ExecuteUseCaseAsync(new CreateGroupUseCaseRequest(request.Name, request.BeginDate, request.EndDate));
            // ToDo return url
            return Ok(groupID);
        }

        [HttpDelete("{groupID}")]
        public async Task<IActionResult> DisbandGroup([FromRoute] Guid groupID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new DisbandGroupUseCaseRequest(groupID));
            return Ok();
        }

        [HttpPost("{groupID}/students")]
        public async Task<IActionResult> AddStudent([FromRoute] Guid GroupID, [FromBody] AddStudentRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new AddStudentUseCaseRequest(GroupID, request.StudentID));
            return Ok();
        }

        [HttpDelete("{groupID}/students/{studentID}")]
        public async Task<IActionResult> ExcludeStudent([FromRoute] Guid groupID, [FromRoute] Guid studentID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new ExcludeStudentUseCaseRequest(groupID, studentID));
            return Ok();
        }
    }
}
