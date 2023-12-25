using Domain.Model; 
using Domain.Contracts; 
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Denonciations
{
    public class CreateDenonciationCommandHandler : IRequestHandler<CreateDenonciationCommand, int>
    {
        private readonly IDenonciationRepository _denonciationRepository;

        public CreateDenonciationCommandHandler(IDenonciationRepository denonciationRepository)
        {
            _denonciationRepository = denonciationRepository;
        }

        public async Task<int> Handle(CreateDenonciationCommand command, CancellationToken cancellationToken)
        {
            var denonciation = new Denonciation
            {
                Informant = new Personne
                {
                    FirstName = command.InformateurFirstName,
                    LastName = command.InformateurLastName,
                },
                Suspect = new Personne
                {
                    FirstName = command.SuspectFirstName,
                    LastName = command.SuspectLastName,
                },
                Offense = command.TypeOfOffense,
                EvasionCountry = command.CountryEvasion
            };

            await _denonciationRepository.CreateDenonciationAsync(denonciation);

            return denonciation.Id;
        }
    }
}
