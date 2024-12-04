using LMS.Client.Integrations.Lesson;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerLessons
{
    public class CreateOwnerLessonSourceCode : ComponentBase
    {
        [Inject] ILessonIntegration LessonIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected CreateLessonModel CreateLessonModel { get; set; } = new CreateLessonModel();

        public async Task CreateLesson()
        {
            var statusCode = await LessonIntegration.AddOwnerLesson(CourseId,CreateLessonModel);
            if(statusCode == HttpStatusCode.OK)
            {
                NavigationManager.NavigateTo($"/owner-lessons/{CourseId}");
            }
        }
        
    }
}
