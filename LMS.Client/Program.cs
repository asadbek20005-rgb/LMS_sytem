using Blazored.LocalStorage;
using LMS.Client;
using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Course;
using LMS.Client.Integrations.Owner;
using LMS.Client.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7168") });
builder.Services.AddScoped<IClientIntegration, ClientIntegration>();
builder.Services.AddScoped<IOwnerIntegration, OwnerIntegration>();
builder.Services.AddScoped<ICourseIntegration, CourseIntegration>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
