using System;
using Domain.Model;

namespace Domain.Contracts
{
    public interface IDenonciationRepository
    {
        Task<Denonciation> CreateDenonciationAsync(Denonciation denonciation);
        Task<IEnumerable<Denonciation>> GetAllDenonciationsAsync();
        Task<Denonciation> GetDenonciationAsync(string UserName, int id);
        Task<IEnumerable<Denonciation>> GetNonTreatedDenonciationsAsync();
        Task UpdateDenonciationAsync(Denonciation denonciation);
        Task DeleteDenonciationAsync(int id);
    }
}
