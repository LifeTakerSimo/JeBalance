using Domain.Contracts;
using Domain.Model;
using JeBalance.Common;
using JeBalance.Configuration;
using JeBalance.SQLLite;
using JeBalance.SQLLite.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DenonciationRepository : IDenonciationRepository
    {
        private readonly DatabaseContext _context;
        private readonly string _connectionString;


        public DenonciationRepository(DatabaseContext context, IOptions<DBSettings> dbConfig)
        {
            _connectionString = dbConfig.Value.DefaultConnection;
            _context = context;
        }

        public async Task<Guid> CreateDenonciationAsync(Denonciation denonciation)
        {
            var denonciationSQLS = denonciation.ToSQLS();
            denonciationSQLS.DenonciationId = Guid.NewGuid(); 
            denonciationSQLS.Timestamp = DateTime.UtcNow.AddMonths(2);

            await _context.Denonciations.AddAsync(denonciationSQLS);
            await _context.SaveChangesAsync();

            return denonciationSQLS.DenonciationId; 
        }


        public async Task<Denonciation> GetDenonciationAsync(string userName, int id)
        {
            var denonciationSQLS = await _context.Denonciations
                .Include(d => d.Informant)
                .Include(d => d.Suspect)
                .SingleOrDefaultAsync(d => d.Id == id);

            return denonciationSQLS?.ToDomain();
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

        public async Task<List<Denonciation>> GetAllDenonciationsWithNoResponseAsync()
        {
            var denonciations = new List<Denonciation>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                    "SELECT * FROM Denonciation AS D " +
                    "INNER JOIN Response AS R ON D.denonciation_id = denonciation_id" +
                    "WHERE response_type IS NULL";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var denonciation = new Denonciation
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        };

                        denonciations.Add(denonciation);
                    }
                }
            }

            return denonciations;
        }
    }
}
