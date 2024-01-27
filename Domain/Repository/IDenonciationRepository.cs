using System;
using Domain.Model;

namespace Domain.Contracts
{
    public interface IDenonciationRepository
    {
        Task<Denonciation> CreateDenonciationAsync(Denonciation denonciation);
        Task<IEnumerable<Denonciation>> GetAllDenonciationsAsync();
        Task<Denonciation> GetDenonciationByIdAsync(int id);
        Task<IEnumerable<Denonciation>> GetNonTreatedDenonciationsAsync();
        Task UpdateDenonciationAsync(Denonciation denonciation);
        Task DeleteDenonciationAsync(int id);
    }
}
