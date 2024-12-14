using LMS.Common.Models;
using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task AddOrUpdateContent(Content content);
        Task<Content> GetContentById(Guid userId, Guid courseId, int lessonId, int contentId);
        Task<List<Content>> GetAllContents(Guid userId, Guid courseId, int lessonId);
        // For Admin
        Task<List<Content>> GetContents();
    }
}
