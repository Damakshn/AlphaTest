using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaTest.Application;
using AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AproveProposal;
using AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AssignProposal;
using AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.DeclineProposal;
using AlphaTest.WebApi.Models.Proposals;
using AlphaTest.WebApi.Utils.Security;

namespace AlphaTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        private ISystemGateway _alphaTest;

        public ProposalsController(ISystemGateway alphaTest)
        {
            _alphaTest = alphaTest;
        }

        [HttpPost("{proposalID}/assign")]
        public async Task<IActionResult> AssignProposal([FromRoute] Guid proposalID)
        {   
            await _alphaTest.ExecuteUseCaseAsync(new AssignProposalUseCaseRequest(proposalID, User.GetID()));
            return Ok();
        }

        [HttpPost("{proposalID}/aprove")]
        public async Task<IActionResult> AproveProposal([FromRoute] Guid proposalID)
        {
            await _alphaTest.ExecuteUseCaseAsync(new AproveProposalUseCaseRequest(proposalID));
            return Ok();
        }

        [HttpPost("{proposalID}/decline")]
        public async Task<IActionResult> DeclineProposal([FromRoute] Guid proposalID, [FromBody] DeclineProposalRequest request)
        {
            await _alphaTest.ExecuteUseCaseAsync(new DeclineProposalUseCaseRequest(proposalID, request.Remark));
            return Ok();
        }
    }
}
