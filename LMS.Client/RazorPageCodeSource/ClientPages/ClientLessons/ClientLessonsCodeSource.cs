using LMS.Client.Integrations.Lesson;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.ClientPages.ClientLessons
{
    public class ClientLessonsCodeSource : ComponentBase
    {
        [Inject] ILessonIntegration LessonIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected List<LessonDto> Lessons { get; set; } = new List<LessonDto>();

        protected override async Task OnInitializedAsync()
        {
            Lessons = await GetAllClientLessons();
        }

        public async Task<List<LessonDto>> GetAllClientLessons()
        {
            var (statustCode, lessonDtos) = await LessonIntegration.GetAllClientLessons(CourseId);
            if (statustCode == System.Net.HttpStatusCode.OK)
                return lessonDtos;

            return new List<LessonDto>();
        }

        public void SeeVideos(int lessonId)
        {
            NavigationManager.NavigateTo($"/content/{CourseId}/{lessonId}");
        }
    }
}
