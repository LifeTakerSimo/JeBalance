using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model; 
using Domain.Contracts;

namespace Domain.Queries.Denonciations
{
    public class GetAllDenonciationsQueryHandler : IRequestHandler<GetAllDenonciationsQuery, List<Denonciation>>
    {
        private readonly IDenonciationRepository _denonciationRepository;

        public GetAllDenonciationsQueryHandler(IDenonciationRepository denonciationRepository)
        {
            _denonciationRepository = denonciationRepository;
        }

        public async Task<List<Denonciation>> Handle(GetAllDenonciationsQuery request, CancellationToken cancellationToken)
        {
            return await _denonciationRepository.GetAllDenonciationsWithNoResponseAsync();
        }
    }
}
