using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Model
{
    public class Person : Entity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public bool IsVIP { get; set; }

        public Person() : base(0)
        {
        }

        public Person(string firstname, string lastname) : this(new Name(firstname), new Name(lastname))
        {
        }

        public Person(Name firstName, Name lastName) : base(0)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Person(int id, string firstName, string lastName, string streetNumber, string streetName, string postalCode, string cityName, bool isVIP)
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
