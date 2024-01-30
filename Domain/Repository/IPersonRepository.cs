using System;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Repository;

public interface IPersonRepository
{
    Task<Person> GetByIdAsync(int id);
    Task<Person> GetByUsernameAsync(string username);
    Task<bool> AddAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task SaveChangesAsync();
}
