using LMS.Common.Dtos;
using System.Net;

namespace LMS.Client.Integrations.Content
{
    public interface IContentIntegration
    {
        Task<Tuple<HttpStatusCode, ContentDto>> GetContent();
    }
}
