﻿using System;
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
    private readonly string _apiBaseUrl = "https://localhost:7286";

    public IndexModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string ApiBaseUrl => _apiBaseUrl;

    public async Task<IActionResult> OnPostCreateDenunciationAsync()
    {
        return RedirectToPage();
    }
}
