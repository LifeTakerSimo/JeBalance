using Domain.Contracts;
using Domain.Model;
using JeBalance.Common;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JeBalance.Repos
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly DatabaseContext _context;

        public ResponseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(Response response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            var denonciation = await _context.Denonciations
                .Include(d => d.Informant)
                .SingleOrDefaultAsync(d => d.DenonciationId == response.DenonciationId);

            if (denonciation == null || await _context.Responses.AnyAsync(r => r.DenonciationId == response.DenonciationId))
            {
                return false; // Denonciation not found or Response already exists
            }

            if (!response.ResponseType)
            {
                var informantToUpdate = await _context.Denonciations
                    .Where(d => d.DenonciationId == response.DenonciationId)
                    .Select(d => d.Informant)
                    .FirstOrDefaultAsync();

                if (informantToUpdate != null)
                {
                    var informantEntity = await _context.Persons
                        .FirstOrDefaultAsync(p => p.Id == informantToUpdate.Id);

                    if (informantEntity != null)
                    {
                        informantEntity.Rejection = (informantEntity.Rejection ?? 0) + 1;
                    }
                }
            }

            denonciation.IsTreated = true;

            try
            {
                ResponseSQLS responseSQLS = response.ToSQLS();
                await _context.Responses.AddAsync(responseSQLS);
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
