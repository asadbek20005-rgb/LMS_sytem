using LMS.Client.Integrations.Content;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.ClientPages.ClientContent
{
    public class SeeVideoCodeSource : ComponentBase
    {
        [Inject] IContentIntegration ContentIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        [Parameter] public int ContentId { get; set; }
        private string ContentType { get; set; }
        protected string VideUrl;

        protected override async Task OnInitializedAsync()
        {
            VideUrl = await SeeVideo(); 
        }

        public async Task<string> SeeVideo()
        {
            var (stream, fileName, contentType) = await ContentIntegration.GetClientContent(CourseId, LessonId, ContentId);
            this.ContentType = contentType;

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            return $"data:{contentType};base64,{Convert.ToBase64String(ms.ToArray())}";
        }

   
    }
}
