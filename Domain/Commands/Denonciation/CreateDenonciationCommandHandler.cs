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
                Informant = new Person(
                    command.Informant.FirstName, command.Informant.LastName,
                    command.Informant.StreetName, command.Informant.StreetNumber,
                    command.Informant.PostalCode, command.Informant.CityName,
                    command.Informant.Email, command.Informant.UserName),

                Suspect = new Person(
                    command.Suspect.FirstName, command.Suspect.LastName,
                    command.Suspect.StreetName, command.Suspect.StreetNumber,
                    command.Suspect.PostalCode, command.Suspect.CityName,
                    command.Suspect.Email, string.Empty), 

                Offense = command.Offense,
                EvasionCountry = command.EvasionCountry
            };

            await _denonciationRepository.CreateDenonciationAsync(denonciation);

            return denonciation.Id;
        }
    }
}
