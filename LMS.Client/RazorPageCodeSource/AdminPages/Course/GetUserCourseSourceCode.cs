using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Course
{
    public class GetUserCourseSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected CourseDto Course { get; set; } = new CourseDto();

        protected override async Task OnInitializedAsync()
        {
            Course = await GetUserCourse();
        }

        public async Task<CourseDto> GetUserCourse()
        {
            var (statusCode, courseDto) = await AdminIntegration.GetUserCourse(UserId, CourseId);
            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                return courseDto;
            }
            return null;
        }

        public void GetCourseLessons()
        {
            NavigationManager.NavigateTo($"/admin-page/course-lessons/{UserId}/{CourseId}");
        }
    }
}
