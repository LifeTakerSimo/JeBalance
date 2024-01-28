using System;
using API.Ressource;
using Domain.Model;

namespace API.Ressource
{
 
    public class DenonciationDTO
    {
        private Denonciation denonciation;

        public DenonciationDTO()
        {
            this.denonciation = denonciation;
        }

        public InformantDTO Informant { get; set; }
        public SuspectDTO Suspect { get; set; }
        public string Offense { get; set; }
        public string EvasionCountry { get; set; }

    }

}

