using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Commands.Persons;
using Domain.Model;
using Domain.Repository;

namespace Domain.Test.Users


{
    public class PersonRepositoryUser : IPersonRepository
    {
        private readonly List<Person> _persons;

        public PersonRepositoryUser()
        {
            _persons = new List<Person>();
        }

        public Task<Person> GetByIdAsync(int id)
        {
            var person = _persons.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(person);
        }

        public Task<Person> GetByUsernameAsync(string username)
        {
            var person = _persons.FirstOrDefault(p => p.UserName == username);
            return Task.FromResult(person);
        }

        public Task<bool> AddAsync(Person person)
        {
            _persons.Add(person);
            return Task.FromResult(true);
        }

        public Task<Person> GetPerson(string firstName, string lastname)
        {
            var person = _persons.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastname);
            return Task.FromResult(person);
        }

        public Task<bool> UpdateAsync(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(p => p.Id == person.Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.UserName = person.UserName;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var personToRemove = _persons.FirstOrDefault(p => p.Id == id);
            if (personToRemove != null)
            {
                _persons.Remove(personToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> ExistsByUsernameAsync(string username)
        {
            return Task.FromResult(_persons.Any(p => p.UserName == username));
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public Task<bool> AddVipCommand(Person person)
        {
            return Task.FromResult(true);
        }

        public Task<bool> DeleteVipCommand(string username)
        {
            return Task.FromResult(true);
        }

        public Task<List<Person>> GetAllVipsAsync()
        {
            return Task.FromResult(_persons.Where(p => p.IsVIP).ToList());
        }
    }
}
