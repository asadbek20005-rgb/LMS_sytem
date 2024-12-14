using LMS.Client.Integrations.Admin;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.User
{
    public class BlogUserSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }

        public async Task BlogUser()
        {
            var statusCode = await AdminIntegration.BlockUser(UserId);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo("/admin-pages/users");

        }
    }
}
