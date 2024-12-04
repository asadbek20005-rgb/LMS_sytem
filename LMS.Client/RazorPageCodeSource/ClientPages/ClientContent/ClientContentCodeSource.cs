using LMS.Client.Integrations.Content;
using LMS.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.ClientPages.ClientContent
{
    public class ClientContentCodeSource : ComponentBase
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
            var (statusCode, contentDtos) = await ContentIntegration.GetClientContents(CourseId, LessonId);
            if (statusCode == System.Net.HttpStatusCode.OK)
                return contentDtos;
            return contentDtos;
        }

        public void SeeVideo(int contentId)
        {
            NavigationManager.NavigateTo($"/seeVideo/{CourseId}/{LessonId}/{contentId}");
        }
    }
}
