using API.Parameters;
using Domain.Commands.Denonciations;
using Domain.Queries.Denonciations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Ressource;

namespace API.Controllers;

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
    public async Task<IActionResult> CreateDenonciation([FromBody] DenonciationDTO resource)
    {
        var command = new CreateDenonciationCommand(
            resource.Informant.FirstName, resource.Informant.LastName, resource.Informant.StreetName, resource.Informant.StreetNumber, resource.Informant.PostalCode, resource.Informant.CityName,
            resource.Informant.Email, resource.Informant.UserName,
            resource.Suspect.FirstName, resource.Suspect.LastName, resource.Suspect.StreetName, resource.Suspect.StreetNumber, resource.Suspect.PostalCode, resource.Suspect.CityName,
            resource.Suspect.Email, 
            resource.Offense, resource.EvasionCountry);
        var denonciationId = await _mediator.Send(command);

        return Ok(new { DenonciationId = denonciationId });
    }

    [HttpGet]
    public async Task<IActionResult> GetDenonciationById([FromQuery] GetDenonciationParameter parameter)
    {
        if (parameter == null || string.IsNullOrWhiteSpace(parameter.UserName) || parameter.Id <= 0)
        {
            return BadRequest("Invalid parameters.");
        }
        var query = new GetDenonciationById(parameter.UserName, parameter.Id);
        var denonciation = await _mediator.Send(query);
        
        if (denonciation == null)
        {
            return NotFound($"Denonciation with ID {parameter.Id} not found.");
        }

        Response.Headers.Add("X-Custom-UserName", parameter.UserName);
        Response.Headers.Add("X-Custom-Id", parameter.Id.ToString());
        return Ok(denonciation);
    }

}