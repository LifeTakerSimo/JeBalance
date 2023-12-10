using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Domain;

/*
 * Class that identifies a DenunciationDTO
 */
public class Denonciation
{
    [JsonProperty("id")]
    public long Indentifiant { get; set; }

    [JsonProperty("informant")]
    public PersonneDTO Informateur { get; set; }

    [JsonProperty("suspect")]
    public PersonneDTO Suspect { get; set; }

    [JsonProperty("type")]
    public string Delit { get; set; }

    [JsonProperty("country")]
    public string PaysEvasion { get; set; }
}
