using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Domain.Model;

namespace API.Parameters;
public class GetDenonciationParameter
{
    public Guid Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public string UserName { get; set; }

    public GetDenonciationParameter()
    {
    }
}


