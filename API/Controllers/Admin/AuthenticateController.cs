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

    public AuthenticateController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        ILogger<AuthenticateController> logger,
        IMediator mediator,
        DatabaseContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mediator = mediator;
        _logger = logger;
        _context = context;
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                username = user.UserName,
                role = userRoles.FirstOrDefault(),
                driverid = user.UserId, // TODO : CHANGE THIS 
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
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

            var hashedPassword = HashPassword(model.Password);

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

            var userId = await _mediator.Send(command); // Dispatch the command

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


    private string HashPassword(string password)
    {
        var hasher = new PasswordHasher<ApplicationUser>(); 
        return hasher.HashPassword(null, password);
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByNameAsync(model.UserName);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Authentication.Response { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Authentication.Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        return Ok(new Authentication.Response { Status = "Success", Message = "Admin created successfully!" });
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
