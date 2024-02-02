using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Domain.Model;

namespace API.Parameters;
public class CreateResponseParameter
{
    public Guid DenonciationId { get; set; }

    public int Amount { get; set; }

    public bool ResponseType { get; set; }

    public CreateResponseParameter()
    {
    }
}


