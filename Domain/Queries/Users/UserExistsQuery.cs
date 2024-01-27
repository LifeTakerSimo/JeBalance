using MediatR;

namespace Domain.Queries.Users
{
    public class UserExistsQuery : IRequest<bool>
    {
        public string Username { get; }

        public UserExistsQuery(string username)
        {
            Username = username;
        }
    }
}
