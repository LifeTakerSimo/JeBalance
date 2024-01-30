using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Domain.Commands;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Domain.Queries.Users;
using JeBalance.SQLLite;
using Microsoft.AspNetCore.Authorization;
using Domain.Service;
using JeBalance.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly ILogger<AuthenticateController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly DatabaseContext _context;
    private readonly IPasswordService _passwordService;

    public AuthenticateController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        ILogger<AuthenticateController> logger,
        IMediator mediator,
        DatabaseContext context,
        IPasswordService passwordService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mediator = mediator;
        _logger = logger;
        _context = context;
        _passwordService = passwordService;

    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        _logger.LogInformation($"Attempting to login user: {model.UserName}");

        try
        {
            var user = await _mediator.Send(new GetUserQuery(model.UserName));
            if (user == null || !_passwordService.VerifyPassword(user.PasswordHash, model.Password))
            {
                _logger.LogWarning($"User {model.UserName} login failed: Invalid credentials.");
                return Unauthorized(new Authentication.Response { Status = "Error", Message = "Invalid credentials!" });
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (user.IsAdmin)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            if (user.IsFisc)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "Fisc"));
            }

            var token = GetToken(authClaims);

            _logger.LogInformation($"User {model.UserName} logged in successfully.");
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error during login: {ex.Message}");

            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Authentication.Response { Status = "Error", Message = "An unexpected error occurred during login." });
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            _logger.LogInformation($"Attempting to register user: {model.UserName}");

            var userExists = await _mediator.Send(new UserExistsQuery(model.UserName));

            if (userExists)
            {
                _logger.LogWarning($"User {model.UserName} already exists.");
                return StatusCode(StatusCodes.Status500InternalServerError, new Authentication.Response { Status = "Error", Message = "User already exists!" });
            }

            var hashedPassword = _passwordService.HashPassword(model.Password);

            var command = new CreateUserCommand(
                model.Email,
                model.UserName,
                hashedPassword,
                model.Role,
                model.FirstName,
                model.LastName,
                model.IsVIP,
                model.IsAdmin,
                model.IsFisc);

            var userId = await _mediator.Send(command);

            _logger.LogInformation($"User {model.UserName} created successfully with ID {userId}.");
            return Ok(new Authentication.Response { Status = "Success", Message = "User created successfully!" });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error during user registration: {ex.Message}");

            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Authentication.Response { Status = "Error", Message = "An unexpected error occurred during user registration." });
        }
    }


    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
