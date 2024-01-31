using API.Parameters;
using Domain.Commands.Denonciations;
using Domain.Queries.Denonciations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Ressource;
using Domain.Commands;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Domain.Queries.Persons;
using Domain.Commands.Responses;

namespace API.Controllers;

//[Authorize(Roles = "Fisc")]
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

    [HttpPost]
    [Route("create_Response")]
    public async Task<IActionResult> CreateResponse([FromBody] CreateDenonciationParameter parameter)
    {
        try
        {
            _logger.LogInformation("Attempting to create a response for Denonciations");
            var addResponseCommand = new CreateResponseCommand(parameter.DenonciationId, parameter.Amount, parameter.ResponseType);
            var result = await _mediator.Send(addResponseCommand);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating a Response for  Denoncaiation : {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

}