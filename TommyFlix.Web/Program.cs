using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TommyFlix.Web;
using TommyFlix.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient para tu API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7280/") // ← cambiá por tu URL
});

// HttpClient nombrado para TMDB
builder.Services.AddHttpClient("tmdb", client =>
{
    client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            builder.Configuration["TmdbConfig:BearerToken"]
        );
});

// AuthServices
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TommyFlixAuthStateProvider>();
builder.Services.AddScoped<AuthService>();

// MyServices
builder.Services.AddScoped<TmdbService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<FavoriteService>();

await builder.Build().RunAsync();