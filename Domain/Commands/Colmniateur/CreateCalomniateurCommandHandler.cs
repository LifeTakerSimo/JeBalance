using MediatR;
using Domain.Repository;
using Domain.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Colmniateur;

public class CreateCalomniateurCommandHandler : IRequestHandler<CreateCalomniateurCommand, int>
{
    private readonly ICalomniateurRepository _caloRepository;

    public CreateCalomniateurCommandHandler(ICalomniateurRepository caloRepository)
    {
        _caloRepository = caloRepository ?? throw new ArgumentNullException(nameof(caloRepository));
    }

    public async Task<int> Handle(CreateCalomniateurCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var Person = new Person
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                UserName = command.Username,

            };

            var calo = new Calomniateur(
                0,
                Person
            );

            await _caloRepository.AddAsync(calo);
            await _caloRepository.SaveChangesAsync();

            return calo.Id;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error occurred when creating calo: {ex.Message}", ex);
        }
    }
}