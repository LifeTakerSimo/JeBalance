using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Repository;

namespace Domain.Commands.Persons
{
    public class DeletePersonCommandHandler : IRequestHandler<DeleteVipCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<bool> Handle(DeleteVipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = await _personRepository.GetByUsernameAsync(request.Username);
                if (person == null)
                {
                    Console.WriteLine($"Person with username {request.Username} not found.");
                    return false;
                }
                await _personRepository.DeleteVipCommand(person.UserName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the person: {ex.Message}");
                return false;
            }
        }
    }
}
