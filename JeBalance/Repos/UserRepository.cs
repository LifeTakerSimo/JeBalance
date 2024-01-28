﻿using Domain.Contracts;
using Domain.Model;
using Domain.Repository;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JeBalance.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var user = await _context.Users // todo : correct the logic here 
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user?.Person;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Person.UserName == username);
            return user;
        }

        public async Task<bool> AddAsync(User user)
        {
            var userSQLS = new UserSQLS
            {
                Person = new PersonSQLS
                {
                    Id = user.Person.Id,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    UserName = user.Person.UserName,
                    StreetNumber = user.Person.StreetNumber,
                    StreetName = user.Person.StreetName,
                    PostalCode = user.Person.PostalCode,
                    CityName = user.Person.CityName,
                    IsVIP = user.IsVip,
                    Email = user.Person.Email,
                    IsAdmin = user.IsAdmin,
                    IsFisc = user.IsFisc,
                },
                PasswordHash = user.PasswordHash,
                IsAdmin = user.IsAdmin,
                IsVip = user.IsVip,
                IsFisc = user.IsFisc
            };

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

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.Person.UserName.ToUpper() == username.ToUpper());

            return userExists;
        }

    }
}
