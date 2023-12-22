using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeBalance.SQLLite.Model
{
    [Table("Response")]
    public class ResponseSQLS
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("response_type")]
        public string ResponseType { get; set; }

        [Column("amount")]
        public decimal? Amount { get; set; }

        [Column("denonciation_id")]
        public int? DenonciationId { get; set; }
        [ForeignKey("DenonciationId")]
        public virtual DenonciationSQLS Denonciation { get; set; }
    }
}

