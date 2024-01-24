using System;
using MediatR;
using Domain.Model;
using Domain;

namespace Domain.Commands;

public class CreateUserCommand : IRequest<int>
{
    public Person Person { get; }


    public CreateUserCommand(string firstname, string lastname)
    {
        Person = new Person(firstname, lastname);
    }
}
