using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Model;
using Domain.Repository;

namespace Domain.Queries.Persons
{
    public class GetVipQueryHandler : IRequestHandler<GetVipQuery, Person>
    {
        private readonly IPersonRepository _personRepository;

        public GetVipQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<Person> Handle(GetVipQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new ArgumentException("Username cannot be null or whitespace.", nameof(request.Username));
            }

            var person = await _personRepository.GetByUsernameAsync(request.Username);
            
            if (person == null)
            {
                throw new KeyNotFoundException($"No person found with username {request.Username}.");
            }

            if (!person.IsVIP)
            {
                throw new InvalidOperationException($"The user with username {request.Username} isn't a VIP.");
            }

            return person;
        }
    }
}
