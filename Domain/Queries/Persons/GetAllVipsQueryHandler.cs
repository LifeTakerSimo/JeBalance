using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Contracts;
using Domain.Queries.Persons;
using Domain.Repository;

namespace Application.Queries.Persons
{
    public class GetAllVipsQueryHandler : IRequestHandler<GetAllVipsQuery, List<Person>>
    {
        private readonly IPersonRepository _personRepository;

        public GetAllVipsQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<List<Person>> Handle(GetAllVipsQuery request, CancellationToken cancellationToken)
        {
            return await _personRepository.GetAllVipsAsync();
        }
    }
}
