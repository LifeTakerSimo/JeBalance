using System;
using Domain.Model;

namespace Domain.Contracts
{
    public interface IResponseRepository
    {
        Task<bool> AddAsync(Response response);

    }
}




