using System;
using MediatR;
using Domain.Model;

namespace Domain.Commands.Denonciations
{
    public class CreateDenonciationCommand : IRequest<int>
    {
        public Person Informant { get; private set; }
        public Person Suspect { get; private set; }
        public string Offense { get; private set; }
        public string EvasionCountry { get; private set; }

        public CreateDenonciationCommand(
        string informantFirstName, string informantLastName, string streetName, string streetNumber, string postalCode, string cityName, string email, string informantUserName,
        string suspectFirstName, string suspectLastName, string sstreetName, string sstreetNumber, string spostalCode, string scityName, string semail,
        string offense, string evasionCountry)
        {
            Informant = new Person(informantFirstName, informantLastName, streetName, streetNumber, postalCode, cityName, email, informantUserName);
            Suspect = new Person(suspectFirstName, suspectLastName, sstreetName, sstreetNumber, spostalCode, scityName, semail, string.Empty);
            Offense = offense;
            EvasionCountry = evasionCountry;
        }
    }
}