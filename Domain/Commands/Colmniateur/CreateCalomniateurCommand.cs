using MediatR;

namespace Domain.Commands.Colmniateur;

public class CreateCalomniateurCommand : IRequest<int>
{
    public string Email { get; }
    public string Username { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public CreateCalomniateurCommand(string email, string username, string password, string firstName, string lastname)
    {
        Email = email;
        Username = username;
        Password = password;
        FirstName = firstName;
        LastName = lastname;
    }
}
