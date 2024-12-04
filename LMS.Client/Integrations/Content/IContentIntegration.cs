using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Content
{
    public interface IContentIntegration
    {
        Task<Tuple<HttpStatusCode, List<ContentDto>>> GetClientContents(Guid courseId, int lessonId);
        Task<(HttpStatusCode, string)> GetClientContent(Guid courseId, int lessonId, int contentId);
        Task<Tuple<HttpStatusCode, List<ContentDto>>> GetOwnerContents(Guid courseId, int lessonId);
        Task<(HttpStatusCode, string)> GetOwnerContent(Guid courseId, int lessonId, int contentId);
        Task<HttpStatusCode> CreateOwnerContent(Guid courseId, int lessonId, CreateContentModel createContentModel);

    }
}
