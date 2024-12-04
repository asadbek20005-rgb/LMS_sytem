using LMS.Client.Integrations.Client;
using LMS.Common.Models.AccountModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Accounts.Client
{
    public class RegisterCodeSource : ComponentBase
    {
        [Inject] private IClientIntegration clientIntegration { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        protected ClientRegisterModel model = new();


        public async Task Register()
        {
            var (statusCode, code) = await clientIntegration.Register(model);
            if (statusCode == System.Net.HttpStatusCode.OK)
                navigationManager.NavigateTo($"/pages/account/client/verify-register/{code}");
        }
    }
}

//public async Task Register()
//{
//    var (statusCode, response) = await UserIntegration.Register(RegisterUserModel);
//    if (statusCode == System.Net.HttpStatusCode.OK)
//    {
//        NavigationManager.NavigateTo("/account/login");
//    }
//    else
//    {
//        NavigationManager.NavigateTo($"/home/{response.ToString()}");
//    }
//}