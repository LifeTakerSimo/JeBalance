using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Model;

namespace Domain.Test.Users
{
    public class ResponseRepositoryUser : IResponseRepository
    {
        private readonly List<Response> _responses;

        public ResponseRepositoryUser()
        {
            _responses = new List<Response>();
        }

        public Task<bool> AddAsync(Response response)
        {
            _responses.Add(response);
            return Task.FromResult(true);
        }
    }
}
