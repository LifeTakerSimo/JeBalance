using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://localhost:5059";

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> OnPostCreateDenunciationAsync(
        string informateurFirstName, string informateurLastName, string informateurStreetName, string informateurStreetNumber, string informateurPostalCode, string informateurCityName, string informateurEmail, string informateurUsername,
        string suspectFirstName, string suspectLastName, string suspectStreetName, string suspectStreetNumber, string suspectPostalCode, string suspectCityName, string suspectEmail,
        string countryEvasion, string typeOfOffense)
    {
        var denonciation = new
        {
            Informant = new
            {
                FirstName = informateurFirstName,
                LastName = informateurLastName,
                StreetName = informateurStreetName,
                StreetNumber = informateurStreetNumber,
                PostalCode = informateurPostalCode,
                CityName = informateurCityName,
                Email = informateurEmail,
                UserName = informateurUsername,
            },
            Suspect = new
            {
                FirstName = suspectFirstName,
                LastName = suspectLastName,
                StreetName = suspectStreetName,
                StreetNumber = suspectStreetNumber,
                PostalCode = suspectPostalCode,
                CityName = suspectCityName,
                Email = suspectEmail,
            },
            Offense = typeOfOffense,
            EvasionCountry = countryEvasion
        };

        var content = new StringContent(JsonSerializer.Serialize(denonciation), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/denonciations", content);

        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Denunciation created successfully.");
            return Content("The denunciation was received successfully.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "There was an error creating the denunciation.");
            return Page();
        }
    }
}
