using Domain.Contracts;
using Domain.Model;
using Domain.Repository;
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


using System.Threading.Tasks;

namespace JeBalance.Repos
{
    public class CalomniateurRepository : ICalomniateurRepository
    {
        private readonly DatabaseContext _context;
        private readonly string _connectionString;

        public CalomniateurRepository(DatabaseContext context, IOptions<DBSettings> dbConfig)
        {
            _context = context;
            _connectionString = dbConfig.Value.DefaultConnection;
        }

        public async Task<bool> IsCalomniateur(string username)
        {
            var caloExists = await _context.Calomniateurs.AnyAsync(c => c.Person.UserName == username);
            return caloExists;
        }

        public async Task<bool> AddAsync(Calomniateur calomniateur)
        {
            var calomniateurSQLS = calomniateur.ToSQLS();

            await _context.Calomniateurs.AddAsync(calomniateurSQLS);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
