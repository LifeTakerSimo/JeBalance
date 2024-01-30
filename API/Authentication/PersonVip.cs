using System;
using System.ComponentModel.DataAnnotations;

namespace API.Authentication;
public class PersonVip
{
    [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "FirstName is required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "this field is required")]
    public bool VIP { get; set; }

    [Required(ErrorMessage = "this field is required")]
    public string Email { get; set; }

}
