using System;
using Domain.Contracts;

namespace Domain.Model
{
    public class Calomniateur : Entity
    {
        public Person Person { get; set; }
        public int Id { get; set; }

        public Calomniateur() : base(0)
        {
        }

        public Calomniateur(int id, Person person)
            : base(id)
        {
            Person = person;
        }
    }
}

