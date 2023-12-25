using System;
using Domain.Contracts;

namespace Domain.Model
{
    public class Calomniateur : Entity
    {
        public Personne Personne { get; set; }
        public int Id { get; set; }

        public Calomniateur() : base(0)
        {
        }

        public Calomniateur(int id, Personne person)
            : base(id)
        {
            Personne = person;
        }
    }
}

