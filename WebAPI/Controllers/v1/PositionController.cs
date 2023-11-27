using Application.Features.Positions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1;

public class PositionController:BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPositionsQuery();
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Add([FromBody] CreatePositionCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}