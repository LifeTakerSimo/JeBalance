using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repository;
using Domain.Model;


namespace Domain.Test.Users;
public class CalomniateurRepositoryUser : ICalomniateurRepository
{
    private readonly Dictionary<string, Calomniateur> _calomniateurs;

    public CalomniateurRepositoryUser()
    {
        _calomniateurs = new Dictionary<string, Calomniateur>();
    }

    public Task<bool> IsCalomniateur(string username)
    {
        if (_calomniateurs.ContainsKey(username))
        {
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> AddAsync(Calomniateur calomniateur)
    {
        if (!_calomniateurs.ContainsKey(calomniateur.Person.UserName))
        {
            _calomniateurs.Add(calomniateur.Person.UserName, calomniateur);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}
