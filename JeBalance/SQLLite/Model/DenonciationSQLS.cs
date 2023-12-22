using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JeBalance.SQLLite.Model
{
    [Table("Denonciation")]
    public class DenonciationSQLS
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public virtual PersonSQLS Informant { get; set; }

        public virtual PersonSQLS Suspect { get; set; }

        [Column("offense")]
        public string Offense { get; set; }

        [Column("evasion_country")]
        public string EvasionCountry { get; set; }

    }
}

