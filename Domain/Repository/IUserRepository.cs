using System;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Repository;

public interface IUserRepository
{
    Task<Person> GetByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task SaveChangesAsync();
}
