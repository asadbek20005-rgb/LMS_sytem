using LMS.Client.Integrations.Course;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerCourses
{
    public class CreateOwnerCourseCodeSource : ComponentBase
    {
        [Inject] private ICourseIntegration CourseIntegration { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        protected CreateCourseModel Model { get; set; } = new();


        public async Task CreateOwnerCourse()
        {
            var statusCode = await CourseIntegration.CreateOwnerCourse(Model);
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                NavigationManager.NavigateTo("/pages/course", forceLoad: true);

            }
        }
    }
}
