using Domain.Model;
using Domain.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repository;

namespace Domain.Commands.Denonciations
{
    public class CreateDenonciationCommandHandler : IRequestHandler<CreateDenonciationCommand, int>
    {
        private readonly IDenonciationRepository _denonciationRepository;
        private readonly IPersonRepository _personRepository;

        public CreateDenonciationCommandHandler(IDenonciationRepository denonciationRepository, IPersonRepository personRepository)
        {
            _denonciationRepository = denonciationRepository;
            _personRepository = personRepository;
        }

        public async Task<int> Handle(CreateDenonciationCommand command, CancellationToken cancellationToken)
        {
            var informantTask = _personRepository.GetByUsernameAsync(command.Informant.UserName);
            var suspectTask = _personRepository.GetByUsernameAsync(command.Suspect.UserName);

            await Task.WhenAll(informantTask, suspectTask);
            var informant = informantTask.Result;
            var suspect = suspectTask.Result;

            if (informant != null && informant.Rejection >= 3)
            {
                throw new InvalidOperationException("Your denunciations got rejected many times.");
            }

            if (suspect?.IsVIP == true)
            {
                throw new InvalidOperationException("You can't denounce a VIP person.");
            }

            informant = new Person
            {
                FirstName = command.Informant.FirstName,
                LastName = command.Informant.LastName,
                StreetNumber = command.Informant.StreetNumber,
                StreetName = command.Informant.StreetName,
                PostalCode = command.Informant.PostalCode,
                CityName = command.Informant.CityName,
                Email = command.Informant.Email,
                IsAdmin = command.Informant.IsAdmin,
                IsFisc = command.Informant.IsFisc,
                Rejection = command.Informant.Rejection,
                UserName = command.Informant.UserName
            };

            suspect ??= new Person
            {
                FirstName = command.Suspect.FirstName,
                LastName = command.Suspect.LastName,
                StreetNumber = command.Suspect.StreetNumber,
                StreetName = command.Suspect.StreetName,
                PostalCode = command.Suspect.PostalCode,
                CityName = command.Suspect.CityName,
                Email = command.Suspect.Email,
                Rejection = command.Suspect.Rejection,
            };

            var denonciation = new Denonciation
            {
                Informant = informant,
                Suspect = suspect,
                Offense = command.Offense,
                EvasionCountry = command.EvasionCountry
            };

            return await _denonciationRepository.CreateDenonciationAsync(denonciation);
        }
    }
}
