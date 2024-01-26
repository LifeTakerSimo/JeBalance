using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Model;

namespace JeBalance.SQLLite.Model
{
    [Table("Person")]
    public class PersonSQLS : Person
    {
        [Column("id")] // todo : ADD ROLE AND USERiD INSTEAD OF ID
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("street_number")]
        public string StreetNumber { get; set; }

        [Column("street_name")]
        public string StreetName { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }

        [Column("city_name")]
        public string CityName { get; set; }

        [Column("is_vip")]
        public bool IsVIP { get; set; }

        [Column("NormalizedUserName")]
        public string NormalizedUserName { get; set; }
        
    }
}

