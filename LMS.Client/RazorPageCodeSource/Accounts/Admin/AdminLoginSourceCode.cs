using LMS.Client.Integrations.Admin;
using LMS.Common.Models.AccountModels;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Accounts.Admin
{
    public class AdminLoginSourceCode: ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        protected AdminLoginModel AdminLoginModel { get; set; }= new AdminLoginModel();
        public async Task Login()
        {
            var statusCode = await AdminIntegration.Login(AdminLoginModel);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo("/admin-pages/users", forceLoad: true);
        }
    }
}
