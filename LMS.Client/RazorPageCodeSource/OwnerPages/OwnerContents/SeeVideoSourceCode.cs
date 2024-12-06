using LMS.Client.Integrations.Content;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components;
using System.IO;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerContents
{
    public class SeeVideoSourceCode : ComponentBase
    {
        [Inject] IContentIntegration ContentIntegration { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        [Parameter] public int ContentId { get; set; }
        protected string Url { get; set; }
        protected string ContentType { get; set; }
        protected AddOrUpdateContentModel AddOrUpdateContentModel { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Url = await SeeVideo();
        }
        public async Task<string> SeeVideo()
        {
            var (stream, fileName, contentType) = await ContentIntegration.GetOwnerContent(CourseId, LessonId, ContentId);
            this.ContentType = contentType;

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            return $"data:{contentType};base64,{Convert.ToBase64String(ms.ToArray())}";
        }

    }
}
