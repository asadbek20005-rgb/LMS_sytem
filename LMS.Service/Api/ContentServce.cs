using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class ContentServce
    {
        private readonly IContentRepository _contentRepository;
        public ContentServce(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task AddOrUpdateContent(Guid userId, Guid courseId, int lessonId, Stream stream, AddOrUpdateContentModel addOrUpdateContentModel)
        {
            await _contentRepository.AddOrUpdateContent(userId, courseId, lessonId, stream, addOrUpdateContentModel);
        }

        public async Task<List<ContentDto>> GetAllContents(Guid userId, Guid courseId, int lessonId)
        {
            var contents = await _contentRepository.GetAllContents(userId, courseId, lessonId);
            return contents.ParseToDtos();
        }

        public async Task<ContentDto> GetContent(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            var content = await _contentRepository.GetContentById(userId, courseId, lessonId, contentId);
            return content.ParseToDto();
        }
    }
}