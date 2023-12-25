using System;
using Domain.Contracts;

namespace Domain.Model
{
    public class Admin : Entity
    {
        public Personne Personne { get; set; }
        public int Id { get; set; }

        public Admin() : base(0)
        {
        }

        public Admin(int id, Personne person)
            : base(id)
        {
            Personne = person;
        }
    }
}
