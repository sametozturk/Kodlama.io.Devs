using Application.Features.Authorizations.Command.Login;
using Application.Features.Authorizations.Command.Register;
using Application.Features.Authorizations.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
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
            AuthorizationRegisterCommand authorizationRegisterCommand = new() { UserForRegisterDto = userForRegisterDto, IpAdress = GetIpAdress() };
            RegisteredDto result = await Mediator.Send(authorizationRegisterCommand);
            return Created("", result.AccessToken);
        }
        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            AuthorizationLoginCommand authorizationLoginCommand = new() { UserForLoginDto = userForLoginDto };
            AuthLoginDto result = await Mediator.Send(authorizationLoginCommand);
            return Ok(result);
        }
    }
}
