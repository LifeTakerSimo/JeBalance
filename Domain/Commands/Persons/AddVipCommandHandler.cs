using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Model;
using Domain.Repository;

namespace Domain.Commands
{
    public class AddVipCommandHandler : IRequestHandler<AddVipCommand, bool>
    {
        private readonly IPersonRepository _personRepository;

        public AddVipCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Handle(AddVipCommand command, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByUsernameAsync(command.UserName);
            if (person != null)
            {
                if (person.IsVIP)
                {
                    throw new InvalidOperationException("The person already exists as a VIP. We are going to make a him a VIP ");
                }
                else
                {
                    person.IsVIP = true;
                    await _personRepository.UpdateAsync(person);
                    return true;
                }
            }
            else
            {
                var newPerson = new Person
                {
                    UserName = command.UserName,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    IsVIP = true,
                    Email = command.Email
                };

                await _personRepository.AddAsync(newPerson);
                return true;
            }
        }
    }
}
