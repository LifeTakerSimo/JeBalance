
using System;

namespace API.Ressource
{
    public class ResponseDTO
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string ResponseType { get; set; }
        public decimal? Amount { get; set; }
        public int? DenonciationId { get; set; }
        public DenonciationDTO Denonciation { get; set; }
    }
}

