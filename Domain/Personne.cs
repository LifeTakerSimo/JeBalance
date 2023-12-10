using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace Domain;

/*
 * Class that identifies a person
 */
public class PersonneDTO
{
    [JsonProperty("id")]
    public string identifiant { get; set; }

    [JsonProperty("first_name")]
    public string Prenom { get; set; }

    [JsonProperty("last_name")]
    public string Nom { get; set; }

    [JsonProperty("address")]
    public Adresse Adresse { get; set; }
}

public class Adresse
{
    [JsonProperty("street_number")]
    public int NumeroVoie { get; set; }

    [JsonProperty("street_name")]
    public string NomVoie { get; set; }

    [JsonProperty("postal_code")]
    public string CodePostal { get; set; }

    [JsonProperty("city_name")]
    public string NomCommune { get; set; }
}