using System;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Groups.CreateGroup;
using AlphaTest.WebApi.Models.Groups;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Groups.DisbandGroup;
using AlphaTest.Application.UseCases.Groups.AddStudent;
using AlphaTest.Application.UseCases.Groups.ExcluedStudent;
using AlphaTest.Application.UseCases.Groups.EditGroupInfo;
using AlphaTest.Application.UseCases.Groups.AssignCurator;
using AlphaTest.Application.UseCases.Groups.UnsetCurator;
using AlphaTest.Application.Models.Users;
using System.Collections.Generic;
using AlphaTest.Application.UseCases.Groups.Queries.StudentsInGroup;

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

        #region Создание групп
        [HttpPost]
        public async Task<IActionResult> CreateNewGroup([FromBody] CreateGroupRequest request)
        {
            Guid groupID = await _alphaTest.ExecuteUseCaseAsync(new CreateGroupUseCaseRequest(request.Name, request.BeginDate, request.EndDate, request.CuratorID));
            // ToDo return url
            return Ok(groupID);
        }
        #endregion

        #region Редактирование, расформирование
        [HttpPut("{groupID}/info")]
        public async Task<IActionResult> EditGroupInfo([FromRoute] Guid groupID, [FromBody] EditGroupRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new EditGroupInfoUseCaseRequest(groupID, request.Name, request.BeginDate, request.EndDate));
            return Ok();
        }

        [HttpDelete("{groupID}")]
        public async Task<IActionResult> DisbandGroup([FromRoute] Guid groupID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new DisbandGroupUseCaseRequest(groupID));
            return Ok();
        }
        #endregion

        #region Добавить/убрать студентов
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
        #endregion

        #region Назначить/убрать куратора
        [HttpPost("{groupID}/curator")]
        public async Task<IActionResult> AssignCurator([FromRoute] Guid groupID, [FromBody] AssignCuratorRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new AssignCuratorUseCaseRequest(groupID, request.CuratorID));
            return Ok();
        }

        [HttpDelete("{groupID}/curator")]
        public async Task<IActionResult> UnsetCurator([FromRoute] Guid groupID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new UnsetCuratorUseCaseRequest(groupID));
            return Ok();
        }
        #endregion

        #region Просмотр информации
        [HttpGet("{groupID}/students")]
        public async Task<List<StudentListItemDto>> ViewStudentsInGroup([FromRoute] Guid groupID)
        {
            StudentsListQuery query = new(groupID);
            return await _alphaTest.ExecuteUseCaseAsync(query);
        }
        #endregion
    }
}
