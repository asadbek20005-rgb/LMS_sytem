using LMS.Client.Integrations.Owner;
using LMS.Common.Models.AccountModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Owner
{
    public class RegisterCodeSource : ComponentBase
    {
        [Inject] private IOwnerIntegration OwnerIntegration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        protected OwnerRegisterModel model = new();


        public async Task Register()
        {
            var (statusCode, code) = await OwnerIntegration.Register(model);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo($"/pages/account/owner/verify-regsiter/{code}");
        }
    }
}