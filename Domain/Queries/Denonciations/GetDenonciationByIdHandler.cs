using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain.Contracts;
using Domain.Model;
using Domain.Repository;

namespace Domain.Queries.Denonciations
{
    public class GetDenonciationByIdHandler : IRequestHandler<GetDenonciationById, (Denonciation, Response)>
    {
        private readonly IDenonciationRepository _denonciationRepository;
        private readonly ILogger<GetDenonciationByIdHandler> _logger;

        public GetDenonciationByIdHandler(IDenonciationRepository denonciationRepository, ILogger<GetDenonciationByIdHandler> logger)
        {
            _denonciationRepository = denonciationRepository;
            _logger = logger;
        }

        public async Task<(Denonciation, Response)> Handle(GetDenonciationById query, CancellationToken cancellationToken)
        {
            try
            {
                var denonciationResponseTuple = await _denonciationRepository.GetDenonciationAsync(query.UserName, query.Id);
                var denonciation = denonciationResponseTuple.Item1;
                var response = denonciationResponseTuple.Item2;

                if (denonciation == null)
                {
                    _logger.LogInformation($"Denonciation with ID {query.Id} was not found.");
                    return (null, null); // Return a tuple with nulls if denonciation is not found
                }

                if (denonciation.Informant == null || denonciation.Informant.UserName != query.UserName)
                {
                    _logger.LogWarning($"User {query.UserName} is not authorized to view denonciation with ID {query.Id}.");
                    throw new UnauthorizedAccessException($"User {query.UserName} is not authorized to access this denonciation.");
                }

                return (denonciation, response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving denonciation with ID {query.Id}: {ex}");
                throw; 
            }
        }


    }
}