using System;
using MediatR;

namespace Domain.Commands.Denonciations
{
    public class CreateDenonciationCommand : IRequest<int>
    {
        public string InformateurFirstName { get; set; }
        public string InformateurLastName { get; set; }
        public string InformateurStreetNumber { get; set; }
        public string InformateurStreetName { get; set; }
        public string InformateurPostalCode { get; set; }
        public string InformateurCityName { get; set; }
        public string SuspectFirstName { get; set; }
        public string SuspectLastName { get; set; }
        public string CountryEvasion { get; set; }
        public string TypeOfOffense { get; set; }

        public CreateDenonciationCommand(string informateurFirstName, string informateurLastName)
        {
            /* TODO : finish this */
            InformateurFirstName = informateurFirstName;
            InformateurLastName = informateurLastName;
        }
    }
}

