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
using System.ComponentModel.DataAnnotations.Schema;
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

        public async Task<(Denonciation, Response)> GetDenonciationAsync(string userName, Guid id)
        {
            var denonciation = await _context.Denonciations
                .Include(d => d.Informant)
                .Include(d => d.Suspect)
                .SingleOrDefaultAsync(d => d.DenonciationId == id && d.Informant.UserName == userName);

            if (denonciation == null) return (null, null);

            var response = await _context.Responses
                .SingleOrDefaultAsync(r => r.DenonciationId == id);

            var denonciationDomain = denonciation.ToDomain();
            var responseDomain = response?.ToDomain();

            return (denonciationDomain, responseDomain);
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
                    "SELECT D.denonciation_id, D.timestamp, D.evasion_country, D.offense, D.isTreated, " +
                    "I.first_name AS informant_first_name, I.last_name AS informant_last_name, " +
                    "S.first_name AS suspect_first_name, S.last_name AS suspect_last_name " +
                    "FROM Denonciation AS D " +
                    "LEFT JOIN Person AS I ON D.InformantId = I.Id " +
                    "LEFT JOIN Person AS S ON D.SuspectId = S.Id " +
                    "WHERE D.isTreated = false " +
                    "ORDER BY D.timestamp";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var denonciation = new DenonciationSQLS
                        {
                            DenonciationId = reader.GetGuid(reader.GetOrdinal("denonciation_id")),
                            Timestamp = reader.GetDateTime(reader.GetOrdinal("timestamp")),
                            Informant = new PersonSQLS
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("informant_first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("informant_last_name")),
                            },
                            Suspect = new PersonSQLS
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("suspect_first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("suspect_last_name")),
                            },
                            EvasionCountry = reader.GetString(reader.GetOrdinal("evasion_country")),
                            Offense = reader.GetString(reader.GetOrdinal("offense")),
                            IsTreated = reader.GetBoolean(reader.GetOrdinal("isTreated"))
                        };

                        Denonciation denonciationNew = denonciation.ToDomain();
                        denonciations.Add(denonciationNew);
                    }
                }

            }

            return denonciations;
        }
    }
}
