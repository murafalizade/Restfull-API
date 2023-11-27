using Application.Features.Users;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1;

public class AuthController:BaseApiController
{
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command) 
        => Ok(await Mediator.Send(command));

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
        => Ok(await Mediator.Send(command));
}