using LMS.Client.Integrations.Content;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Net.Http;

namespace LMS.Client.RazorPageCodeSource.ClientPages.ClientContent
{
    public class SeeVideoCodeSource : ComponentBase
    {
        [Inject] IContentIntegration ContentIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        [Parameter] public int ContentId { get; set; }

        protected string VideUrl;

        protected override async Task OnInitializedAsync()
        {
            VideUrl = await SeeVideo();
        }

        public async Task<string> SeeVideo()
        {
            var (statusCode, stream) = await ContentIntegration.GetClientContent(CourseId, LessonId, ContentId);

            if (statusCode == System.Net.HttpStatusCode.OK)
            {

                return stream;
            }

            return null;
        }

        public async Task<string> GetVideoUrl(Guid courseId, int lessonId, int contentId)
        {
            // API endpoint orqali video fayli URL olish
            var client = HttpClientFactory.Create();
            var url = $"{NavigationManager.BaseUri}/api/clients/clientId/courses/{courseId}/lessons/{lessonId}ClientContents/{contentId}";

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Video fayli uchun URL ni olish
                return url;
            }

            return null;
        }
    }
}
