using Domain.Commands.Denonciations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DenonciationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DenonciationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDenonciation([FromBody] CreateDenonciationCommand command)
    {
        var id = await _mediator.Send(command);
        return Created(string.Empty, new { id });
    }
}