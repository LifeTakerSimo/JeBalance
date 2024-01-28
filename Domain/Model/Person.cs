using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Model
{
    public class Person : Entity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? StreetNumber { get; set; }
        public string? StreetName { get; set; }
        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public bool IsVIP { get; set; }
        public string Email { get; set; }
        public bool IsFisc { get; set; }
        public bool IsAdmin { get; set; }

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

        public Person(int id, string firstName, string lastName, string streetNumber, string streetName, string postalCode, string cityName, bool isVIP, string userName, string email, bool isAdmin, bool isFisc)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            StreetNumber = streetNumber;
            StreetName = streetName;
            PostalCode = postalCode;
            CityName = cityName;
            IsVIP = isVIP;
            UserName = userName;
            Email = email;
            IsFisc = isFisc;
            IsAdmin = isAdmin;
        }

        public Person(string informantFirstName, string informantLastName, string streetName, string streetNumber, string postalCode, string cityName, string email, string userName)
        : base(0)
        {
            FirstName = informantFirstName;
            LastName = informantLastName;
            StreetName = streetName;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            CityName = cityName;
            Email = email;
            IsVIP = false;
            UserName = userName;
            IsFisc = false;
            IsAdmin = false;
        }

    }
}
