using System;
using Domain.Contracts;

namespace Domain.Model
{
    public class Admin : Entity
    {
        public Person Person { get; set; }
        public int Id { get; set; }

        public Admin() : base(0)
        {
        }

        public Admin(int id, Person person)
            : base(id)
        {
            Person = person;
        }
    }
}
