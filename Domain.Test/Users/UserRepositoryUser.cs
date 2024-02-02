using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository;
using Domain.Model;
using Domain.Repository;

namespace Domain.Test.Users

{
    public class UserRepositoryUser : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepositoryUser()
        {
            _users = new List<User>();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username);
            return Task.FromResult<User>(user);
        }

        public Task<bool> AddAsync(User user)
        {
            _users.Add(user);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var userToRemove = _users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> ExistsByUsernameAsync(string username)
        {
            return Task.FromResult(_users.Any(u => u.UserName == username));
        }

        public Task SaveChangesAsync()
        {

            return Task.CompletedTask;
        }

        public Task<Person> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
