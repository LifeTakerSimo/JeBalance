using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Model;

namespace JeBalance.SQLLite.Model
{
    [Table("Calomniateur")]
    public class CalomniateurSQLS : Calomniateur
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("person_id")]
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual PersonSQLS Person { get; set; }
    }
}


