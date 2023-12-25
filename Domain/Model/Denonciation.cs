using System;
using Domain.Contracts;

namespace Domain.Model
{
    public class Denonciation : Entity
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Personne Informant { get; set; }
        public Personne Suspect { get; set; }
        public string Offense { get; set; }
        public string EvasionCountry { get; set; }
        public Response DenonciationResponse { get; set; } 

        public Denonciation() : base(0)
        {
        }

        public Denonciation(int id, DateTime timestamp, Personne informant, Personne suspect, string offense, string evasionCountry)
            : base(id)
        {
            Timestamp = timestamp;
            Informant = informant;
            Suspect = suspect;
            Offense = offense;
            EvasionCountry = evasionCountry;
        }
    }
}

