using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Commands.Responses;
using Domain.Contracts;
using Domain.Model;

namespace Domain.Commands.Responses
{
    public class CreateResponseCommandHandler : IRequestHandler<CreateResponseCommand, bool> // Adjust return type as needed
    {
        private readonly IResponseRepository _responseRepository; // Assuming such an interface exists

        public CreateResponseCommandHandler(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;
        }

        public async Task<bool> Handle(CreateResponseCommand command, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                DenonciationId = command.DenonciationId,
                Amount = command.Amount,
                ResponseType = command.ResponseType,
                Timestamp = DateTime.UtcNow,
            };

            await _responseRepository.AddAsync(response);
            return true;
        }
    }
}
