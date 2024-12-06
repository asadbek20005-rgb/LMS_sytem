using LMS.Client.Integrations.Content;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Net;

namespace LMS.Client.RazorPageCodeSource.OwnerPages.OwnerContents
{
    public class CreateOwnerContentSourceCode : ComponentBase
    {
        [Inject] IContentIntegration ContentIntegration { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public Guid CourseId { get; set; }
        [Parameter] public int LessonId { get; set; }
        private IBrowserFile BrowserFile;
        private const long MaxFileSize = 300 * 1024 * 1024;
        protected AddOrUpdateContentModel AddOrUpdateContentModel { get; set; } = new AddOrUpdateContentModel();

        protected async Task OnInputFileChange(InputFileChangeEventArgs args)
        {
            BrowserFile = args.File;

            using (var memoryStream = new MemoryStream())
            {
                await BrowserFile.OpenReadStream(MaxFileSize).CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var bytes = memoryStream.ToArray();

                AddOrUpdateContentModel.FormFile = new FormFile(
                    new MemoryStream(bytes),  
                    baseStreamOffset: 0,
                    length: bytes.Length,
                    name: BrowserFile.Name,
                    fileName: BrowserFile.Name
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = BrowserFile.ContentType
                };
            }

        }


        public async Task CreateOwnerContent()
        {
            var statusCode = await ContentIntegration.CreateOwnerContent(CourseId, LessonId, AddOrUpdateContentModel, BrowserFile);
            if (statusCode == HttpStatusCode.OK)
                NavigationManager.NavigateTo("/pages/course/owner");
        }
    }
}
