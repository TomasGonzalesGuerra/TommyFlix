using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TommyFlix.Web;
using TommyFlix.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient para tu propia API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7000/") // ? cambiá por tu URL de API
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

builder.Services.AddScoped<TmdbService>();

await builder.Build().RunAsync();