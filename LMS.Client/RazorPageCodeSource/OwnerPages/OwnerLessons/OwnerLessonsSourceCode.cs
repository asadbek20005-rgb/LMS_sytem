using LMS.Client.Integrations.Lesson;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerLessons
{
    public class OwnerLessonsSourceCode : ComponentBase
    {
        [Inject] ILessonIntegration LessonIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
        protected override async Task OnInitializedAsync()
        {
            await GetAllCourseLessons();
        }

        public async Task GetAllCourseLessons()
        {
            var (statusCode, lessonDtos) = await LessonIntegration.GetAllOwnerLessons(CourseId);

            if(statusCode == System.Net.HttpStatusCode.OK)
            {
                Lessons = lessonDtos;
            }
        }

        public async Task SeeVideos(int lessonId)
        {
            NavigationManager.NavigateTo($"/owner-contents/{CourseId}/{lessonId}");
        }

        public void NavigateToCreateLesson()
        {
            NavigationManager.NavigateTo($"/create-owner-lesson/{CourseId}");
        }
    }
}
