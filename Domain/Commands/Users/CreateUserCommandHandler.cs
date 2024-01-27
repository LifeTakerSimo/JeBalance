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
    // private readonly IRoleRepository _roleRepository;

    public CreateUserCommandHandler(IUserRepository userRepository /*, IRoleRepository roleRepository*/)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        // _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var hashedPassword = HashPassword(command.Password);

            var person = new Person
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserName = command.Username
            };

            var user = new User(
                0, 
                person,
                command.Role,
                hashedPassword,
                command.Username
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // If you have role management, handle it here
            // Example: await _roleRepository.AssignRoleAsync(user.Id, command.Role);

            return user.Id; // Ensure this gets set after saving changes
        }
        catch (Exception ex)
        {
            // Improved error handling as needed
            throw new InvalidOperationException($"Error occurred when creating user: {ex.Message}", ex);
        }
    }

    private string HashPassword(string password)
    {
        // Replace this with a secure password hashing mechanism
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
    }
}
// todo : handle role and remove comments 
