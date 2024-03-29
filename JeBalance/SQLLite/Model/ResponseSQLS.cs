﻿using Domain.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JeBalance.SQLLite.Model
{
    [Table("Response")]
    public class ResponseSQLS : Response
    {
        [Key]
        [Column("id")]
        public new int Id { get; set; }

        [Column("timestamp")]
        public new DateTime Timestamp { get; set; }

        [Column("response_type")]
        public new bool ResponseType { get; set; }

        [Column("amount")]
        public new decimal? Amount { get; set; }

        public Guid DenonciationId { get; set; }

    }
}

