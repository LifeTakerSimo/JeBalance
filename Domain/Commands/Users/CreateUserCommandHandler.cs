using MediatR;
using Domain.Repository;
using Domain.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Person = new Person
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserName = command.Username,
                IsVIP = command.IsVip,
                IsAdmin = command.IsAdmin,
                IsFisc = command.IsFisc,

            };

            var user = new User(
                0, 
                Person,
                command.Password,
                command.Username,
                Person.IsAdmin,
                Person.IsFisc,
                Person.IsVIP
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user.Id;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error occurred when creating user: {ex.Message}", ex);
        }
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}