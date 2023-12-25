using System;
using System.Net;
using Domain.Contracts;

namespace Domain.Model
{
    public class Personne : Entity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public bool IsVIP { get; set; }

        public Personne() : base(0)
        {
        }

        public Personne(int id, string firstName, string lastName, string streetNumber, string streetName, string postalCode, string cityName, bool isVIP)
            : base(id) 
        {
            FirstName = firstName;
            LastName = lastName;
            StreetNumber = streetNumber;
            StreetName = streetName;
            PostalCode = postalCode;
            CityName = cityName;
            IsVIP = isVIP;
        }
    }
}
