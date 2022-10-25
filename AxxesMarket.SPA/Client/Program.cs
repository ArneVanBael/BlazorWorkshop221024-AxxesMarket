using AxxesMarket.SPA.Client;
using AxxesMarket.SPA.Client.BFF;
using AxxesMarket.SPA.Client.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// authentication state and authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

// HTTP client configuration
builder.Services.AddTransient<AntiforgeryHandler>();
builder.Services.AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AntiforgeryHandler>();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

// blazor state setup

builder.Services.AddScoped<Translator>();
builder.Services.AddScoped<BlazorHttpClient>();

builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");

var host = builder.Build();


// set the default culture by checking if cookie exist, if not exists add cookie with default culture (functions for getting and settings cookie are in app.js)
// set the default culture and supported culture list
var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
var result = await jsInterop.InvokeAsync<string>("getCookie", ".AspNetCore.Culture");
CultureInfo culture;
if (result != null)
{
    culture = new CultureInfo(result);
}
else
{
    await jsInterop.InvokeVoidAsync("setCookie", ".AspNetCore.Culture", "en-US", 100);
    culture = new CultureInfo("en-US");
}
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;


await host.RunAsync();
