using Application.Features.GitHubProfiles.Command.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Command.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Command.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
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
    }
}
