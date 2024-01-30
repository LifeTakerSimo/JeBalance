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
    public class GetDenonciationByIdHandler : IRequestHandler<GetDenonciationById, Denonciation>
    {
        private readonly IDenonciationRepository _denonciationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetDenonciationByIdHandler> _logger;

        public GetDenonciationByIdHandler(IDenonciationRepository denonciationRepository, IUserRepository userRepository, ILogger<GetDenonciationByIdHandler> logger)
        {
            _denonciationRepository = denonciationRepository;
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<Denonciation> Handle(GetDenonciationById query, CancellationToken cancellationToken)
        {
            try
            {
                var denonciation = await _denonciationRepository.GetDenonciationAsync(query.UserName, query.Id);
                if (denonciation == null)
                {
                    _logger.LogInformation($"Denonciation with ID {query.Id} was not found.");
                    return null;
                }

                if (denonciation.Informant == null || denonciation.Informant.UserName != query.UserName)
                {
                    _logger.LogWarning($"User {query.UserName} is not authorized to view denonciation with ID {query.Id}.");
                    throw new UnauthorizedAccessException($"User {query.UserName} is not authorized to access this denonciation.");
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