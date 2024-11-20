using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Owner;
using LMS.Common.OTPModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Owner
{
    public class VerifyRegisterCodeSource : ComponentBase
    {
        [Inject] IOwnerIntegration OwnerIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public string Code { get; set; }
        protected OtpModel OtpModel { get; set; } = new();


        protected async void VerifyRegister()
        {
            var statusCode = await OwnerIntegration.VerifyRegister(OtpModel);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo("/pages/account/owner/login");

        }
    }
}
