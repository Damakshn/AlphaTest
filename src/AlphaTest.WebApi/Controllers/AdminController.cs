using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AlphaTest.WebApi.Models.Admin.UserManagement;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.CreateUser;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SetRoles;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SuspendUser;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.GenerateTemporaryPassword;
using System.Collections.Generic;
using AlphaTest.Application.UseCases.Admin.Commands.UserManagement.StudentBulkImport;
using AlphaTest.Core.Users.BulkImportReport;
using AlphaTest.WebApi.Utils.Security;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public AdminController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateNewUser(CreateUserRequest request)
        {

            Guid userID = await _alphaTest.ExecuteUseCaseAsync(
                new CreateUserUseCaseRequest(
                    request.FirstName, 
                    request.LastName, 
                    request.MiddleName, 
                    request.Email, 
                    request.TemporaryPassword, 
                    request.InitialRole));
            return Ok(userID);
        }

        [HttpPut("users/{userID}/roles")]
        public async Task<IActionResult> SetUserRoles([FromRoute] Guid userID, [FromBody] SetRolesRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new SetRoleUseCaseRequest(userID, request.Roles));
            return Ok();
        }

        [HttpDelete("users/{userID}")]
        public async Task<IActionResult> SuspendUser([FromRoute] Guid userID)
        {
            await _alphaTest.ExecuteUseCaseAsync(
                new SuspendUserUseCaseRequest(
                    userID,
                    User.GetID()));
            return Ok();
        }

        [HttpPost("users/{userID}/repeatPasswordGeneration")]
        public async Task<IActionResult> RepeatPasswordGeneration([FromRoute] Guid userID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new GenerateTemporaryPasswordUseCaseRequest(userID));
            return Ok();
        }

        [HttpPost("users/bulkImport")]
        public async Task<IActionResult> BulkImport([FromBody] List<ImportStudentRequestData> request)
        {
            List<BulkImportReportLine> report = await _alphaTest.ExecuteUseCaseAsync(new BulkImportUseCaseRequest(request));
            return new JsonResult(report);
        }
    }
}
