using System;
namespace API.Ressource;


public class PersonDTO
{
    public int UserId { get; set; }
    public int RoleId { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string PostalCode { get; set; }
    public string CityName { get; set; }
    public bool IsVIP { get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
}
