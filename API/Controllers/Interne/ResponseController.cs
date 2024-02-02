using API.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commands.Responses;

namespace API.Controllers;

[Authorize(Roles = "Fisc")]
[ApiController]
[Route("api/[controller]")]
public class ResponseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ResponseController> _logger;

    public ResponseController(IMediator mediator, ILogger<ResponseController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /*
     * Allows to create a response of the denonciation if u are a fisc
     */
    [Authorize(Roles = "Fisc")]
    [HttpPost]
    [Route("create_Response")]
    public async Task<IActionResult> CreateResponse([FromBody] CreateResponseParameter parameter)
    {
        try
        {
            _logger.LogInformation("Attempting to create a response for Denonciations");
            var addResponseCommand = new CreateResponseCommand(parameter.DenonciationId, parameter.Amount, parameter.ResponseType);
            var result = await _mediator.Send(addResponseCommand);

            if (result)
            {
                return Ok("Response created successfully.");
            }
            else
            {
                return BadRequest("A response already exists for this denonciations.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating a Response for  Denoncaiation : {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

}