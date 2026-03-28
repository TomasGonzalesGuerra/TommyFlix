using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using TommyFlix.Shared.DTOs;
using System.Net.Http.Json;

namespace TommyFlix.Web.Services;

public class AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
{
    private readonly HttpClient _http = http;
    private readonly ILocalStorageService _localStorage = localStorage;
    private readonly AuthenticationStateProvider _authStateProvider = authStateProvider;

    public async Task<(bool success, string? error)> LoginAsync(LoginDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/accounts/login", model);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }

        var token = await response.Content.ReadFromJsonAsync<TokenDTO>();
        await _localStorage.SetItemAsync("authToken", token!.Token!);
        ((TommyFlixAuthStateProvider)_authStateProvider).NotifyUserAuthentication(token.Token!);
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        return (true, null);
    }

    public async Task<(bool success, string? error)> RegisterAsync(UserDTO model)
    {
        var response = await _http.PostAsJsonAsync("api/accounts/createuser", model);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }

        var token = await response.Content.ReadFromJsonAsync<TokenDTO>();
        await _localStorage.SetItemAsync("authToken", token!.Token!);
        ((TommyFlixAuthStateProvider)_authStateProvider).NotifyUserAuthentication(token.Token!);
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        return (true, null);
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((TommyFlixAuthStateProvider)_authStateProvider).NotifyUserLogout();
        _http.DefaultRequestHeaders.Authorization = null;
    }
}
