using LMS.Client.Integrations.Client;
using LMS.Client.Integrations.Course;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.ClientPages.ClientCourse
{
    public class ClientCourseCodeSource : ComponentBase
    {
        [Inject] ICourseIntegration CourseIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected List<CourseDto> Courses { get; set; } = new List<CourseDto>();

        protected override async Task OnInitializedAsync()
        {
            Courses = await GetClientCourses();
        }

        public async Task<List<CourseDto>> GetClientCourses()
        {
            var (statusCode, courseDtos) = await CourseIntegration.GetAllClientCourses();
            if (statusCode == System.Net.HttpStatusCode.OK)
                return courseDtos;
            return new List<CourseDto>();
        }

          public void SelectedCourse(Guid courseId)
        {
            NavigationManager.NavigateTo($"/lessons/{courseId}");
        }
    }
}
