using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Owner;
using LMS.Common.Models.AccountModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Owner
{
    public class LoginCodeSource : ComponentBase
    {
        [Inject] IOwnerIntegration OwnerIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected OwnerLoginModel model { get; set; } = new();

        public async Task Login()
        {

            var (statusCode, code) = await OwnerIntegration.Login(model);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo($"/pages/account/owner/verify-login/{code}");
        }
    }
}
