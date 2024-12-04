using LMS.Client.Integrations.Client;
using LMS.Common.OTPModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Accounts.Client
{
    public class VerifyLoginCodeSource : ComponentBase
    {
        [Inject] IClientIntegration ClientIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public string Code { get; set; }
        protected OtpModel OtpModel { get; set; } = new();

        protected async void VerifyLogin()
        {
            var statusCode = await ClientIntegration.VerifyLogin(OtpModel);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo("/pages/course", forceLoad: true);
        }
    }
}
