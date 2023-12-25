using Domain.Contracts;
using Domain.Model;
using JeBalance.SQLLite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DenonciationRepository : IDenonciationRepository
    {
        private readonly DatabaseContext _context;

        public DenonciationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Denonciation> CreateDenonciationAsync(Denonciation denonciation)
        {
            _context.Denonciations.Add((JeBalance.SQLLite.Model.DenonciationSQLS)denonciation);
            await _context.SaveChangesAsync();
            return denonciation;
        }

        public async Task<IEnumerable<Denonciation>> GetAllDenonciationsAsync()
        {
            return await _context.Denonciations.ToListAsync();
        }

        public async Task<Denonciation> GetDenonciationByIdAsync(int id)
        {
            return await _context.Denonciations.FindAsync(id);
        }

        public async Task<IEnumerable<Denonciation>> GetNonTreatedDenonciationsAsync()
        {
            return await _context.Denonciations
                .Where(d => d.DenonciationResponse == null)
                .ToListAsync();
        }

        public async Task UpdateDenonciationAsync(Denonciation denonciation)
        {
            _context.Entry(denonciation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDenonciationAsync(int id)
        {
            var denonciation = await _context.Denonciations.FindAsync(id);
            if (denonciation != null)
            {
                _context.Denonciations.Remove(denonciation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
