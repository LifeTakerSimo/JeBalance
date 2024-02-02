using System;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Repository;

public interface ICalomniateurRepository
{
    Task<bool> IsCalomniateur(string username);
    Task<bool> AddAsync(Calomniateur calomniateur);
    Task SaveChangesAsync();
}
