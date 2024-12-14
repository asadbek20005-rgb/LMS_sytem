using LMS.Client.Integrations.Admin;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.User
{
    public class UnBlogUserSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter] public Guid UserId { get; set; }

        public async Task UnBlogUser()
        {
            var statusCode = await AdminIntegration.UnBlockUser(UserId);
            if (statusCode == System.Net.HttpStatusCode.OK)
                NavigationManager.NavigateTo("/admin-pages/users");
        }
    }
}
