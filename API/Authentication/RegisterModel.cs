using System.ComponentModel.DataAnnotations;

namespace API.Authentication;

public class RegisterModel
{
    [Required(ErrorMessage = "Firstame is required")]
    public string? Firstname { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    public string? Lastname { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string? Role { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}

