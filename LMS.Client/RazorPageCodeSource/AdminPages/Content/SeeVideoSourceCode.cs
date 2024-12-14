using LMS.Client.Integrations.Admin;
using Microsoft.AspNetCore.Components;

namespace LMS.Client.RazorPageCodeSource.AdminPages.Content
{
    public class SeeVideoSourceCode : ComponentBase
    {
        [Inject] IAdminIntegration AdminIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid UserId { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        [Parameter] public int ContentId { get; set; }



        private string ContentType { get; set; }
        protected string? VideUrl = null;

        protected override async Task OnInitializedAsync()
        {
            VideUrl = await SeeVideo();
        }

        public async Task<string> SeeVideo()
        {
            var (stream, fileName, contentType) = await AdminIntegration.GetLessonContent(UserId, CourseId, LessonId, ContentId);
            if(string.IsNullOrEmpty(fileName) || stream == Stream.Null || string.IsNullOrEmpty(contentType))
            {
                return string.Empty;
            }
            this.ContentType = contentType;

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            ms.Position = 0;

            return $"data:{contentType};base64,{Convert.ToBase64String(ms.ToArray())}";
        }


        public async Task DeleteVideo()
        {
            var statusCode = await AdminIntegration.DeleteContent(UserId, CourseId, LessonId, ContentId);
            if (statusCode == System.Net.HttpStatusCode.OK)
            {

                NavigationManager.NavigateTo($"/admin-pages/contents/{UserId}/{CourseId}/{LessonId}");
            }
            
        }
    }
}
