using LMS.Client;
using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Owner;
using LMS.Client.RazorPageCodeSource.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7168") });
builder.Services.AddScoped<IClientIntegration, ClientIntegration>();
builder.Services.AddScoped<IOwnerIntegration, OwnerIntegration>();
await builder.Build().RunAsync();
