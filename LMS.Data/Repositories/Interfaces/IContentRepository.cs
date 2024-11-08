using LMS.Common.Models;
using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task AddOrUpdateContent(Guid userId,Guid courseId,int lessonId,Stream stream, AddOrUpdateContentModel addOrUpdateContentModel);
        Task<Content> GetContentById(Guid userId, Guid courseId, int lessonId, int contentId);
        Task<List<Content>> GetAllContents(Guid userId, Guid courseId, int lessonId);
    }
}
