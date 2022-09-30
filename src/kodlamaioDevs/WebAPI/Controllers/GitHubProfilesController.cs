using Application.Features.GitHubProfiles.Command.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Command.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Command.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfileDynamic;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubProfilesController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromBody] CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createGitHubProfileCommand);
            return Created("", result);
        }

        [HttpPost("delete/{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGitHubProfileQuery getListGitHubProfileQuery = new() { PageRequest = pageRequest };
            GitHubProfileListModel result = await Mediator.Send(getListGitHubProfileQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListGitHubProfileDynamicQuery getListGitHubProfileQuery = new() { PageRequest = pageRequest,Dynamic=dynamic };
            GitHubProfileListModel result = await Mediator.Send(getListGitHubProfileQuery);
            return Ok(result);
        }
    }
}
