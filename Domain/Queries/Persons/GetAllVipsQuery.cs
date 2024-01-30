using MediatR;
using System.Collections.Generic;
using Domain.Model;

namespace Domain.Queries.Persons
{
    public class GetAllVipsQuery : IRequest<List<Person>>
    {
    }
}
