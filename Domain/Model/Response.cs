using System;
namespace Domain.Model
{
    public class Response
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ResponseType { get; set; }
        public decimal? Amount { get; set; }
        public Guid DenonciationId { get; set; }
    }
}

