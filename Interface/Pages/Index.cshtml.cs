using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Model;
using System.Diagnostics;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://localhost:5059";

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> OnPostCreateDenunciationAsync(string informateurFirstName, string informateurLastName, string countryEvasion, string typeOfOffense)
    {
        var denonciation = new
        {
            Informant = new
            {
                FirstName = informateurFirstName,
                LastName = informateurLastName,
            },
            Suspect = new
            {
            },
            Offense = typeOfOffense,
            EvasionCountry = countryEvasion
        };

        var content = new StringContent(JsonSerializer.Serialize(denonciation), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/denonciations", content);

        if (response.IsSuccessStatusCode)
        {
            Debug.WriteLine($"Informateur: {informateurFirstName} {informateurLastName}, Country of Evasion: {countryEvasion}, Type of Offense: {typeOfOffense}");
            return Content("The denunciation was received successfully.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "There was an error creating the denonciation.");
            return Page();
        }
    }
}