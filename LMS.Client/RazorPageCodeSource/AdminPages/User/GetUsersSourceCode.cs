using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.User
{
    public class GetUsersSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected List<UserDto> UsersDtos = new List<UserDto>();

        protected override async Task OnInitializedAsync()
        {
            UsersDtos = await GetUsers();
        }
        public async Task<List<UserDto>> GetUsers()
        {
            var (statusCode, userDtos) = await AdminIntegration.GetAllUsers();
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                return userDtos;
            }
            return new List<UserDto>();
        }

        public void BlogUser(Guid userId)
        {
            NavigationManager.NavigateTo($"/admin-pages/block-user/{userId}");
        }

        public void UnblogUser(Guid userId)
        {
            NavigationManager.NavigateTo($"/admin-pages/unblock-user/{userId}");
        }

        public void GetUserCourses(Guid userId)
        {
            NavigationManager.NavigateTo($"/admin-page/user-courses/{userId}");
        }
    }
}
