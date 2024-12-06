using LMS.Client.Integrations.Course;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerCourses
{
    public class GetAllOwnerCoursesCodeSource : ComponentBase
    {
        [Inject] private ICourseIntegration CourseIntegration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        protected List<CourseDto> Courses { get; set; } = new List<CourseDto>();

        protected override async Task OnInitializedAsync()
        {
            Courses = await GetAllOwnerCourses();
        }

        protected async Task<List<CourseDto>> GetAllOwnerCourses()
        {
            var (statusCode, dtos) = await CourseIntegration.GetAllOwnerCourses();

            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                return dtos;
            }

            return null;
        }

        public void SelectOwnerCourse(Guid courseId)
        {
            NavigationManager.NavigateTo($"/owner-lessons/{courseId}");
        }
    }
}
