using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Contracts;
using Domain.Model;

namespace Domain.Queries.Denonciations
{
    public class GetDenonciationByIdHandler : IRequestHandler<GetDenonciationById, Denonciation>
    {
        private readonly IDenonciationRepository _repository;
        private readonly ILogger<GetDenonciationByIdHandler> _logger;

        public GetDenonciationByIdHandler(IDenonciationRepository repository, ILogger<GetDenonciationByIdHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Denonciation> Handle(GetDenonciationById query, CancellationToken cancellationToken)
        {
            try
            {
                var denonciation = await _repository.GetDenonciationAsync(query.UserName, query.Id);
                if (denonciation == null)
                {
                    _logger.LogInformation($"Denonciation with ID {query.Id} was not found.");
                }
                return denonciation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving denonciation with ID {query.Id}: {ex}");
                throw;
            }
        }
    }
}
