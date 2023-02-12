using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using portfolio.Client;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddLocalization();

var host = builder.Build();
var js = host.Services.GetRequiredService<IJSRuntime>();
var language = await js.InvokeAsync<string>("language.get");

if(language == null)
{
    var languageDefault = new CultureInfo("es-CO");
    CultureInfo.DefaultThreadCurrentCulture = languageDefault;
    CultureInfo.DefaultThreadCurrentUICulture = languageDefault;
}
else
{
    var languageUser = new CultureInfo(language);
    CultureInfo.DefaultThreadCurrentCulture = languageUser;
    CultureInfo.DefaultThreadCurrentUICulture = languageUser;
}

await builder.Build().RunAsync();
