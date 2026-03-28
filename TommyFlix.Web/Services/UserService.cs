using Blazored.LocalStorage;
using TommyFlix.Shared.DTOs;
using TommyFlix.Shared.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TommyFlix.Web.Services;

public class UserService(HttpClient http, ILocalStorageService localStorage)
{
    private readonly HttpClient _http = http;
    private readonly ILocalStorageService _localStorage = localStorage;

    private async Task SetAuthHeaderAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<User?> GetProfileAsync()
    {
        await SetAuthHeaderAsync();
        return await _http.GetFromJsonAsync<User>("api/accounts/profile");
    }

    public async Task<(bool success, string? error)> UpdateProfileAsync(User user)
    {
        await SetAuthHeaderAsync();
        var response = await _http.PutAsJsonAsync("api/accounts", user);
        if (!response.IsSuccessStatusCode)
            return (false, await response.Content.ReadAsStringAsync());
        return (true, null);
    }

    public async Task<(bool success, string? error)> ChangePasswordAsync(ChangePasswordDTO model)
    {
        await SetAuthHeaderAsync();
        var response = await _http.PostAsJsonAsync("api/accounts/changepassword", model);
        if (!response.IsSuccessStatusCode)
            return (false, await response.Content.ReadAsStringAsync());
        return (true, null);
    }
}