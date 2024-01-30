using Domain.Contracts;
using Domain.Model;
using Domain.Repository;
using JeBalance.Common;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JeBalance.Repos
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext _context;

        public PersonRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            return person;
        }

        public async Task<Person> GetByUsernameAsync(string username)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.UserName == username);
            return person;
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            var userExists = await _context.Persons
                .AnyAsync(u => u.UserName.ToUpper() == username.ToUpper());

            return userExists;
        }

        public async Task<bool> AddAsync(User user)
        {
            var userSQLS = user.ToSQLS();

            await _context.Users.AddAsync(userSQLS);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
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

    }
}
