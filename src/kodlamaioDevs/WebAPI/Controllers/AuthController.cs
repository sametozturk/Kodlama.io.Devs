using Application.Features.Authorizations.Command.Login;
using Application.Features.Authorizations.Command.Register;
using Application.Features.Authorizations.Dtos;
using Core.Security.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            AuthorizationRegisterCommand authorizationRegisterCommand = new() { UserForRegisterDto = userForRegisterDto };
            AuthRegisterDto result = await Mediator.Send(authorizationRegisterCommand);
            return Created("", result.AccessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            AuthorizationLoginCommand authorizationLoginCommand = new() { UserForLoginDto = userForLoginDto }; //ipAddress = getIpAddress() };
            AuthLoginDto result = await Mediator.Send(authorizationLoginCommand);
            return Ok(result);
        }
    }
}
