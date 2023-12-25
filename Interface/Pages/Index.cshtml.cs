using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Model;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "https://your-api-url.com"; // Replace with your API's base URL.

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
                // Add other properties of Informant as needed
            },
            Suspect = new
            {
                // Set properties of the suspect
            },
            Offense = typeOfOffense,
            EvasionCountry = countryEvasion
        };

        var content = new StringContent(JsonSerializer.Serialize(denonciation), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{_apiBaseUrl}/denonciations", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("ConfirmationPage");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "There was an error creating the denonciation.");
            return Page();
        }
    }
}
