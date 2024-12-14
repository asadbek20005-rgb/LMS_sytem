using LMS.Client.Integrations.Admin;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Content
{
    public class LessonContentsSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }

        protected List<ContentDto> Contents { get; set; } = new List<ContentDto>();

        protected override async Task OnInitializedAsync()
        {
            Contents = await GetLessonContents();
        }
        public async Task<List<ContentDto>> GetLessonContents()
        {
            var (statusCode, contentDtos) = await AdminIntegration.GetLessonContents(UserId, CourseId, LessonId);
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                return contentDtos;
            }
            return new List<ContentDto>();
        }


        public void GetLessonContent(int contentId)
        {
            NavigationManager.NavigateTo($"/admin-page/see-video/{UserId}/{CourseId}/{LessonId}/{contentId}");
        }
    }
}
