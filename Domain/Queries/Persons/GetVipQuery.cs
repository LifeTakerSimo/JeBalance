using MediatR;
using Domain.Model;

namespace Domain.Queries.Persons
{
    public class GetVipQuery : IRequest<Person>
    {
        public string Username { get; }

        public GetVipQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
