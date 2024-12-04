using LMS.Client.Integrations.Owner;
using LMS.Common.OTPModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Accounts.Owner
{
    public class VerifyLoginCodeSource : ComponentBase
    {
        [Inject] IOwnerIntegration OwnerIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public string Code { get; set; }
        protected OtpModel OtpModel { get; set; } = new();

        protected async void VerifyLogin()
        {
            var statusCode = await OwnerIntegration.VerifyLogin(OtpModel);
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                NavigationManager.NavigateTo("/pages/course", forceLoad: true);
            }

        }
    }
}
