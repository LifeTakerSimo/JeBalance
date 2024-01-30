using Domain.Contracts;
using Domain.Model;
using Domain.Repository;
using JeBalance.Common;
using JeBalance.Configuration;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


using System.Threading.Tasks;

namespace JeBalance.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;
        private readonly string _connectionString;

        public PersonRepository(DatabaseContext context, IOptions<DBSettings> dbConfig)
        {
            _context = context;
            _connectionString = dbConfig.Value.DefaultConnection;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<Person> GetByUsernameAsync(string username)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.UserName == username);
            return person?.ToDomain();
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            var userExists = await _context.Persons
                .AnyAsync(u => u.UserName.ToUpper() == username.ToUpper());

            return userExists;
        }

        public async Task<bool> AddAsync(Person person)
        {
            var personSQLS = person.ToSQLS();

            await _context.Persons.AddAsync(personSQLS);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddVipCommand(Person person)
        {
            PersonSQLS personSqls = person.ToSQLS();
            _context.Persons.Add(personSqls);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteVipCommand(string username)
        {
            var personSqls = await _context.Persons.FirstOrDefaultAsync(p => p.UserName == username);
            if (personSqls == null) throw new KeyNotFoundException("Person not found.");

            _context.Persons.Remove(personSqls);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Person> GetVIPByUsernameAsync(string username)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.UserName == username);
            if (!person.IsVIP) throw new Exception("The user isn't a VIP");
            return person;
        }

        public async Task<List<Person>> GetAllVipsAsync()
        {
            var vips = new List<Person>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Person WHERE is_vip = 1";
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var person = new PersonSQLS
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                            LastName = reader.GetString(reader.GetOrdinal("last_name")),
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            IsVIP = reader.GetBoolean(reader.GetOrdinal("is_vip")),
                        };
                        Person newPerson = person.ToDomain();
                        vips.Add(newPerson);
                    }
                }
                
            }

            return vips;
        }
    }
}
