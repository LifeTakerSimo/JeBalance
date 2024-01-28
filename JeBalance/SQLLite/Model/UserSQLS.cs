using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Model;

namespace JeBalance.SQLLite.Model
{
    [Table("User")]
    public class UserSQLS : User
    {
        [Column("person_id")]

        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual PersonSQLS Person { get; set; }
    }
}
