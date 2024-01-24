using System;
namespace API.Authentication;
using Microsoft.AspNetCore.Identity;


public class ApplicationUser : IdentityUser
{
    public int UserId { get; set; }
    public string Role { get; internal set; }
}