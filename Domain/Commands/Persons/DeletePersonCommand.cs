using MediatR;

namespace Domain.Commands.Persons
{
    public class DeleteVipCommand : IRequest<bool>
    {
        public string Username { get; }

        public DeleteVipCommand(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
