using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;

public class IndexModel : PageModel
{
    private readonly HttpClient _httpClient;

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IActionResult> OnPostCreateDenunciationAsync(string informateurFirstName, string informateurLastName,
    int informateurNumeroVoie, string informateurNomVoie, string informateurCodePostal, string informateurNomCommune,
    string suspectFirstName, string suspectLastName, int suspectNumeroVoie, string suspectNomVoie,
    string suspectCodePostal, string suspectNomCommune, string selectedTypeOfOffense, string countryEvasion)
    {
        try
        {
            Denonciation denonciation = new Denonciation
            {
                Informateur = new PersonneDTO
                {
                    Nom = informateurLastName,
                    Prenom = informateurFirstName, 
                    Adresse = new Adresse
                    {
                        NumeroVoie = informateurNumeroVoie,
                        NomVoie = informateurNomVoie, 
                        CodePostal = informateurCodePostal,
                        NomCommune = informateurNomCommune
                    }
                },
                Suspect = new PersonneDTO
                {
                    Nom = suspectLastName, 
                    Prenom = suspectFirstName, 
                    Adresse = new Adresse
                    {
                        NumeroVoie = suspectNumeroVoie,
                        NomVoie = suspectNomVoie, 
                        CodePostal = suspectCodePostal, 
                        NomCommune = suspectNomCommune
                    }
                },
                Delit = selectedTypeOfOffense, 
                PaysEvasion = countryEvasion 
            };

            var jsonDenunciation = JsonSerializer.Serialize(denonciation);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://your-api-endpoint-url"),
                Content = new StringContent(jsonDenunciation, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            // Check if the request was successful (status code 200-299)
            if (response.IsSuccessStatusCode)
            {
                // Process the successful response here
                // For example, show a success message or redirect to another page
                return RedirectToPage("/SuccessPage");
            }
            else
            {
                // Handle the error response from the API (non-successful status code)
                // For example, log the error or display an error message
                // You can access response.StatusCode, response.ReasonPhrase, etc.
                // and handle the error accordingly
                // For now, let's redirect back to the same page
                return RedirectToPage("/Index");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur during the API call
            // Log the exception or display an error message
            // For now, let's redirect back to the same page
            return RedirectToPage("/Index");
        }
    }

}