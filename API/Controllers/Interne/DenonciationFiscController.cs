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

namespace API.Controllers;

//[Authorize(Roles = "Fisc")]
[ApiController]
[Route("api/[controller]")]
public class DenonciationFiscController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DenonciationFiscController> _logger;


    public DenonciationFiscController(IMediator mediator, ILogger<DenonciationFiscController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Authorize(Roles = "Fisc")]
    [HttpGet]
    [Route("get_all_denonciations")]
    public async Task<IActionResult> GetAllDenonciationNoResponse()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve details for all Denonciations");
            var query = new GetAllDenonciationsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving Denoncaiations details: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

}