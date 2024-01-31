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

            var existingResponse = await _context.Responses
                .FirstOrDefaultAsync(r => r.DenonciationId == response.DenonciationId);

            if (existingResponse != null)
            {
                return false; 
            }

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
