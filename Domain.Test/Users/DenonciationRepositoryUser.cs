using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using Domain.Contracts;


namespace Domain.Test.Users
{
    public class DenonciationRepositoryUser : IDenonciationRepository
    {
        private readonly Dictionary<Guid, Denonciation> _denonciations;

        public DenonciationRepositoryUser()
        {
            _denonciations = new Dictionary<Guid, Denonciation>();
        }

        public Task<Guid> CreateDenonciationAsync(Denonciation denonciation)
        {
            Guid id = Guid.NewGuid();
            _denonciations.Add(id, denonciation);
            return Task.FromResult(id);
        }

        public Task<(Denonciation, Response)> GetDenonciationAsync(string userName, Guid id)
        {
            if (_denonciations.TryGetValue(id, out Denonciation denonciation))
            {
                return Task.FromResult((denonciation, new Response())); 
            }
            throw new KeyNotFoundException("Denonciation not found.");
        }

        public Task UpdateDenonciationAsync(Denonciation denonciation)
        {
            if (_denonciations.ContainsKey(denonciation.DenonciationId))
            {
                _denonciations[denonciation.DenonciationId] = denonciation;
                return Task.CompletedTask;
            }
            throw new KeyNotFoundException("Denonciation not found.");
        }

        public Task DeleteDenonciationAsync(Guid id)
        {
            if (_denonciations.ContainsKey(id))
            {
                _denonciations.Remove(id);
                return Task.CompletedTask;
            }
            throw new NotImplementedException();
        }

        public Task<List<Denonciation>> GetAllDenonciationsWithNoResponseAsync()
        {
            return Task.FromResult(new List<Denonciation>(_denonciations.Values));
        }

        public Task DeleteDenonciationAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

