using System.ComponentModel.DataAnnotations.Schema;
using Domain.Model;

namespace JeBalance.SQLLite.Model
{
    [Table("Person")]
    public class PersonSQLS : Person
    {
        [Column("first_name")]
        public new string FirstName { get; set; }

        [Column("last_name")]
        public new string LastName { get; set; }

        [Column("street_number")]
        public new string? StreetNumber { get; set; }

        [Column("street_name")]
        public new string? StreetName { get; set; }

        [Column("postal_code")]
        public new string? PostalCode { get; set; }

        [Column("city_name")]
        public new string? CityName { get; set; }

        [Column("is_vip")]
        public new bool IsVIP { get; set; }

        [Column("is_admin")]
        public new bool IsAdmin { get; set; }

        [Column("is_fisc")]
        public new bool IsFisc { get; set; }

        [Column("UserName")]
        public new string? UserName { get; set; }

        [Column("rejection")]
        public new int? Rejection { get; set; }

    }
}

