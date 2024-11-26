using LMS.Client.Integrations.Client;
using LMS.Common.Models.AccountModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Client
{
    public class LoginCodeSource : ComponentBase
    {
        [Inject] IClientIntegration ClientIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected ClientLoginModel model { get; set; } = new();

        public async Task Login()
        {
            var (statusCode, code) = await ClientIntegration.Login(model);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo($"/pages/account/client/verify-login/{code}");
        }
    }
}
