using MediatR;

namespace Domain.Commands;

public class CreateUserCommand : IRequest<int>
{
    public string Email { get; }
    public string Username { get; }
    public string Password { get; }
    public string Role { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public CreateUserCommand(string email, string username, string password, string role, string firstName, string lastname)
    {
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        FirstName = firstName;
        LastName = lastname;
    }
}
