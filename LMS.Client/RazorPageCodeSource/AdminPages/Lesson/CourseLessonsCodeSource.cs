using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Lesson
{
    public class CourseLessonsCodeSource : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected List<LessonDto> Lessons { get; set; }= new List<LessonDto>();
        protected override async Task OnInitializedAsync()
        {
            Lessons = await GetCourseLessons();
        }

        public async Task<List<LessonDto>> GetCourseLessons()
        {
            var (statusCode,courseLessons) = await AdminIntegration.GetCourseLessons(UserId, CourseId);
            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                return courseLessons;
            }

            return new List<LessonDto>();
        }

        public void GetCourseLesson(int lessonId)
        {
            NavigationManager.NavigateTo($"/admin-page/course-lesson/{UserId}/{CourseId}/{lessonId}");
        }
    }
}
