using System;
namespace Domain.Model
{
    public class Response
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ResponseType { get; set; }
        public decimal? Amount { get; set; }
        public int? DenonciationId { get; set; }
        public Denonciation? Denonciation { get; set; }
    }
}

