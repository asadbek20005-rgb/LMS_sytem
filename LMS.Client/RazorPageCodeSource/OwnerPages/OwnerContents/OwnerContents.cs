using LMS.Client.Integrations.Content;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerContents
{
    public class OwnerContents : ComponentBase
    {
        [Inject] IContentIntegration ContentIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public int LessonId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        protected List<ContentDto> Contents { get; set; } = new List<ContentDto>();

        protected override async Task OnInitializedAsync()
        {

            Contents = await GetContents();
        }

        public async Task<List<ContentDto>> GetContents()
        {
            var (statusCode, contentDtos) = await ContentIntegration.GetOwnerContents(CourseId, LessonId);
            if (statusCode == System.Net.HttpStatusCode.OK)
                return contentDtos;
            return contentDtos;
        }

        public void SeeVideo(int contentId)
        {
            NavigationManager.NavigateTo($"/see-vedio-owner/{CourseId}/{LessonId}/{contentId}");
        }

        public void GotoCreateContent()
        {
            NavigationManager.NavigateTo($"owner-create-content/{CourseId}/{LessonId}");
        }
    }
}
