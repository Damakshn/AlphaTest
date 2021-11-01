using System;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Groups.CreateGroup;
using AlphaTest.WebApi.Models.Groups;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    }
}
