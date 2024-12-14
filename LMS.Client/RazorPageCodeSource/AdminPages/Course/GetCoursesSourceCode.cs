using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Course
{
    public class GetCoursesSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }


        protected List<CourseDto> Courses { get; set; } = new List<CourseDto>();

        protected override async Task OnInitializedAsync()
        {
            Courses = await GetCourses();
        }
        public async Task<List<CourseDto>> GetCourses()
        {
            var (statusCode, courseDtos) = await AdminIntegration.GetAllCourses();
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                return courseDtos;
            }
            return new List<CourseDto>();
        }
    }
}
