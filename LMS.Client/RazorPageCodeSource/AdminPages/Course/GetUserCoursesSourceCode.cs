using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Course
{
    public class GetUserCoursesSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        protected List<CourseDto> UserCourses { get; set; } = new List<CourseDto>();

        protected override async Task OnInitializedAsync()
        {
            UserCourses = await GetUserCourses();
        }

        public async Task<List<CourseDto>> GetUserCourses()
        {
            var (statusCode, userCourseDto) = await AdminIntegration.GetUserCourses(UserId);
            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                return userCourseDto;
            }
            return new List<CourseDto>();
        }


        public void GetCourse(Guid courseId)
        {
            NavigationManager.NavigateTo($"/admin-page/user-course/{UserId}/{courseId}");
        }
    }
}
