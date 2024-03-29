﻿using System;
using Domain.Model;

namespace Domain.Contracts
{
    public interface IDenonciationRepository
    {
        Task<Guid> CreateDenonciationAsync(Denonciation denonciation);
        Task<(Denonciation, Response)> GetDenonciationAsync(string userName, Guid id);
        Task UpdateDenonciationAsync(Denonciation denonciation);
        Task DeleteDenonciationAsync(int id);
        Task<List<Denonciation>> GetAllDenonciationsWithNoResponseAsync();

    }
}
