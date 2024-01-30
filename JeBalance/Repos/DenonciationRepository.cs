using Domain.Contracts;
using Domain.Model;
using JeBalance.Common;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<int> CreateDenonciationAsync(Denonciation denonciation)
        {
            var denonciationSQLS = denonciation.ToSQLS();
            denonciationSQLS.Timestamp = DateTime.UtcNow.AddMonths(2);

            await _context.Denonciations.AddAsync(denonciationSQLS);
            await _context.SaveChangesAsync();

            return denonciationSQLS.Id;
        }

        public async Task<IEnumerable<Denonciation>> GetAllDenonciationsAsync()
        {
            return await _context.Denonciations.ToListAsync();
        }

        public async Task<Denonciation> GetDenonciationAsync(string userName, int id)
        {
            var denonciationSQLS = await _context.Denonciations
                .Include(d => d.Informant)
                .Include(d => d.Suspect)
                .SingleOrDefaultAsync(d => d.Id == id);

            return denonciationSQLS?.ToDomain();
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
