using LMS.Common.Dtos;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace LMS.Client.Integrations.Content
{
    public interface IContentIntegration
    {
        Task<Tuple<HttpStatusCode, List<ContentDto>>> GetClientContents(Guid courseId, int lessonId);
        Task<(HttpStatusCode, string)> GetClientContent(Guid courseId, int lessonId, int contentId);
        Task<Tuple<HttpStatusCode, List<ContentDto>>> GetOwnerContents(Guid courseId, int lessonId);
        Task<(Stream, string, string)> GetOwnerContent(Guid courseId, int lessonId, int contentId);
        Task<HttpStatusCode> CreateOwnerContent(Guid courseId, int lessonId, AddOrUpdateContentModel addOrUpdateContentModel, IBrowserFile file);

    }
}
