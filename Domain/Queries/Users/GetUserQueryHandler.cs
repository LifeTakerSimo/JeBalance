using Domain.Model;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Queries.Users;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GetUserQueryHandler> _logger;

    public GetUserQueryHandler(IUserRepository userRepository, ILogger<GetUserQueryHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.UserName);
            if (user == null)
            {
                _logger.LogInformation($"User {request.UserName} was not found.");
            }
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving user {request.UserName}: {ex}");
            throw;
        }
    }
}
