using MediatR;

namespace Domain.Commands
{
    public class AddVipCommand : IRequest<bool> 
    {
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool Vip { get; private set; }
        public string Email { get; private set; }


        public AddVipCommand(string userName, string firstName, string lastName, bool vip, string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Vip = vip;
            Email = email;

        }
    }
}
