using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Lesson
{
    public class GetCourseLessonCodeSource : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        protected LessonDto Lesson { get; set; } = new LessonDto();

        protected override async Task OnInitializedAsync()
        {
            Lesson = await GetCourseLesson();
        }
        public async Task<LessonDto> GetCourseLesson()
        {
            var (statusCode, lessonDto) = await AdminIntegration.GetCourseLesson(UserId, CourseId, LessonId);
            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                return lessonDto;   
            }
            return null;
        }


        public void GetLessonContents()
        {
            NavigationManager.NavigateTo($"/admin-pages/contents/{UserId}/{CourseId}/{LessonId}");
        }
    }
}
