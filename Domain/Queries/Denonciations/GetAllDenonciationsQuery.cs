using MediatR;
using Domain.Model; 

namespace Domain.Queries.Denonciations
{
    public class GetAllDenonciationsQuery : IRequest<List<Denonciation>>
    {
    }
}
