using System;
using Domain.Model;
using MediatR;

namespace Domain.Queries.Users;

public class GetUserQuery : IRequest<User>
{
    public string UserName { get; }

    public GetUserQuery(string userName)
    {
        UserName = userName;
    }
}
