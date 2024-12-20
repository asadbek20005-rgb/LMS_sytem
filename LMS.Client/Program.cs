using Blazored.LocalStorage;
using LMS.Client;
using LMS.Client.BlazorCustomAuth;
using LMS.Client.Helper;
using LMS.Client.Integrations.Admin;
using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Content;
using LMS.Client.Integrations.Course;
using LMS.Client.Integrations.Lesson;
using LMS.Client.Integrations.Owner;
using LMS.Client.Integrations.Payment;
using LMS.Client.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.IdentityModel.Tokens.Jwt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7168") });
builder.Services.AddScoped<IClientIntegration, ClientIntegration>();
builder.Services.AddScoped<IOwnerIntegration, OwnerIntegration>();
builder.Services.AddScoped<ICourseIntegration, CourseIntegration>();
builder.Services.AddScoped<IPaymentIntegration, PaymentIntegration>();
builder.Services.AddScoped<ILessonIntegration, LessonIntegration>();
builder.Services.AddScoped<IContentIntegration , ContentIntegration>();
builder.Services.AddScoped<IAdminIntegration, AdminIntegration>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<TokenHelper>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
