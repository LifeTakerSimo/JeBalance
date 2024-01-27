using System.ComponentModel.DataAnnotations;

namespace API.Authentication;

public class RegisterModel
{
    [Required(ErrorMessage = "Firstame is required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Lastname is required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string? Role { get; set; }

    [Required(ErrorMessage = "User Name is required")]
    public string? UserName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "IsVIP")]
    public string? IsVIP { get; set; }
}

