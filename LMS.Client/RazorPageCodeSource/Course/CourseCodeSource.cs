using LMS.Client.Integrations.Course;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.Course
{
    public class CourseCodeSource : ComponentBase
    {
        [Inject] private ICourseIntegration CourseIntegration { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        protected List<CourseDto> Courses { get; set; } = new List<CourseDto>();

        protected override async Task OnInitializedAsync()
        {
            Courses = await GetAllCourses();
        }

        protected async Task<List<CourseDto>> GetAllCourses()
        {
            var (statusCode, dtos) = await CourseIntegration.GetAllCourses();
            if(statusCode == System.Net.HttpStatusCode.OK) 
                return dtos;
            return new List<CourseDto>();   
        }

        protected void SelectedCourse(Guid courseId)
        {
            NavigationManager.NavigateTo($"/payment/{courseId}");
        }
       
    }
}
