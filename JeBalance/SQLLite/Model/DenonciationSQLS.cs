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
        public new int Id { get; set; }

        [Column("timestamp")]
        public new DateTime Timestamp { get; set; }

        public virtual PersonSQLS Informant { get; set; }

        public virtual PersonSQLS Suspect { get; set; }

        [Column("offense")]
        public new string Offense { get; set; }

        [Column("evasion_country")]
        public new string EvasionCountry { get; set; }

        public virtual ResponseSQLS DenonciationResponse { get; set; }

        [Column("denonciation_id")]
        public Guid DenonciationId { get; set; }


    }
}

