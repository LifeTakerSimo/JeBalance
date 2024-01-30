using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Commands.Persons;
using Domain.Queries.Persons;
using API.Authentication;
using Domain.Commands;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMediator _mediator;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger<AdminController> logger,
        IMediator mediator)
    {
        _userManager = userManager;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("add-vip")]
    public async Task<IActionResult> AddVip([FromBody] PersonVip command)
    {
        try
        {
            _logger.LogInformation($"Attempting to make person: {command.UserName} a VIP");

            var addVipCommand = new AddVipCommand(command.UserName, command.FirstName, command.LastName, command.VIP, command.Email);

            var result = await _mediator.Send(addVipCommand);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding VIP: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

    [HttpDelete("delete-vip/{userName}")]
    public async Task<IActionResult> DeleteVip(string userName)
    {
        try
        {
            _logger.LogInformation($"Attempting to delete VIP: {userName}");
            var command = new DeleteVipCommand (userName);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting VIP: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

    [HttpGet("get-vip/{userName}")]
    public async Task<IActionResult> GetVip(string userName)
    {
        try
        {
            _logger.LogInformation($"Attempting to retrieve VIP details for: {userName}");
            var query = new GetVipQuery(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving VIP details: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

    [HttpGet("get-all-vips")]
    public async Task<IActionResult> GetAllVips()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve details for all VIPs");
            var query = new GetAllVipsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving VIP details: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }
}
