using System;
namespace Domain.Contracts;


public abstract class Entity
{
    public int Id { get; }

    public Entity(int id) => Id = id;
}

