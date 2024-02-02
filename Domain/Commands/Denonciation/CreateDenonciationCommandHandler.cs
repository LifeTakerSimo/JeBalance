using Domain.Model;
using Domain.Contracts;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Repository;

namespace Domain.Commands.Denonciations
{
    public class CreateDenonciationCommandHandler : IRequestHandler<CreateDenonciationCommand, Guid>
    {
        private readonly IDenonciationRepository _denonciationRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICalomniateurRepository _calomniateurRepository;


        public CreateDenonciationCommandHandler(IDenonciationRepository denonciationRepository, IPersonRepository personRepository, ICalomniateurRepository calomniateurRepository)
        {
            _denonciationRepository = denonciationRepository;
            _personRepository = personRepository;
            _calomniateurRepository = calomniateurRepository;
        }

        public async Task<Guid> Handle(CreateDenonciationCommand command, CancellationToken cancellationToken)
        {
            var informantTask = _personRepository.GetByUsernameAsync(command.Informant.UserName);
            var suspectTask = _personRepository.GetPerson(command.Suspect.FirstName, command.Suspect.LastName);

            await Task.WhenAll(informantTask, suspectTask);
            var informant = informantTask.Result;
            var suspect = suspectTask.Result;

            if (suspect?.IsVIP == true || informant.Rejection >= 3)
            {
                var isCalomniateur = await _calomniateurRepository.IsCalomniateur(informant.UserName);
                if (!isCalomniateur)
                {

                    var Person = new Person
                    {
                        FirstName = informant.FirstName,
                        LastName = informant.LastName,
                        Email = informant.Email,
                        UserName = informant.UserName,

                    };

                    var calo = new Calomniateur(
                        0,
                        Person
                    );
                    await _calomniateurRepository.AddAsync(calo);
                }

                throw new InvalidOperationException("You can't denounce a VIP person or Your denunciations got rejected many times");
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
