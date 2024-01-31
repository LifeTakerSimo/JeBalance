using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Domain.Model;

namespace JeBalance.SQLLite.Model
{
    [Table("Denonciation")]
    public class DenonciationSQLS : Denonciation
    {
        [Column("id")]
        public  int Id { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public virtual PersonSQLS Informant { get; set; }

        public virtual PersonSQLS Suspect { get; set; }

        [Column("offense")]
        public string Offense { get; set; }

        [Column("evasion_country")]
        public string EvasionCountry { get; set; }

        [Column("denonciation_id")]
        public Guid DenonciationId { get; set; }

        [Column("isTreated")]
        public bool IsTreated { get; set; }


    }
}

