using MediatR;
using Domain.Repository;
using System.Threading;
using System.Threading.Tasks;
using Domain.Queries.Users;

namespace Domain.Handlers.Users
{
    public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public UserExistsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(UserExistsQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.ExistsByUsernameAsync(query.Username);
        }
    }
}
