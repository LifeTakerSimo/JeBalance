using System;
using MediatR;
using Domain.Model;

namespace Domain.Queries.Denonciations
{
    public class GetDenonciationById : IRequest<Denonciation>
    {
        public string UserName { get; private set; }
        public Guid Id { get; private set; }

        public GetDenonciationById(string userName, Guid id)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name must not be null or whitespace.", nameof(userName));
            }

            if (id == null)
            {
                throw new ArgumentException("ID must be greater than 0.", nameof(id));
            }

            UserName = userName;
            Id = id;
        }
    }
}
