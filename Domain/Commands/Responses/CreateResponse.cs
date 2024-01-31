using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Domain.Commands.Responses
{
    public class CreateResponseCommand : IRequest<bool>
    {
        public Guid DenonciationId { get; }
        public decimal? Amount { get; }
        public bool ResponseType { get; }

        public CreateResponseCommand(Guid denonciationId, decimal? amount, bool responseType)
        {
            DenonciationId = denonciationId;
            Amount = amount;
            ResponseType = responseType;
        }
    }
}
