using System;
using System.Threading.Tasks;
using Domain.Commands.Persons;
using Domain.Model;

namespace Domain.Repository;

public interface IPersonRepository
{
    Task<Person> GetByIdAsync(int id);
    Task<Person> GetByUsernameAsync(string username);
    Task<bool> AddAsync(Person person);
    Task<bool> UpdateAsync(Person person);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task SaveChangesAsync();

    // VIP requests
    Task<bool> AddVipCommand(Person person);
    Task<bool>DeleteVipCommand(string username);
    Task<List<Person>> GetAllVipsAsync();
}
