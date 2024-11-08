using LMS.Common.Models;
using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;

namespace LMS.Data.Repositories.Implementations
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILessonRepository _lessonRepository;
        private readonly GoogleDrive.GoogleDrive _googleDrive;
        public ContentRepository(AppDbContext appDbContext, GoogleDrive.GoogleDrive googleDrive, IContentRepository contentRepository, IUserRepository userRepository, ILessonRepository lessonRepository)
        {
            _context = appDbContext;
            _googleDrive = googleDrive;
            _lessonRepository = lessonRepository;
        }
        public async Task AddOrUpdateContent(Guid userId, Guid courseId, int lessonId, Stream stream, AddOrUpdateContentModel addOrUpdateContentModel)
        {
            var checking = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);

            var fileId = await _googleDrive.UploadFile(stream, addOrUpdateContentModel.FileName, addOrUpdateContentModel.ContentType);
            var newContent = new Content
            {
                GoogleDriveFileId = fileId,
                Name = addOrUpdateContentModel.Name,
            };
            await _context.Contents.AddAsync(newContent);
            await _context.SaveChangesAsync();
        }

        public async Task<Content> GetContentById(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            var content = userCourseLesson.Contents?.SingleOrDefault(x => x.Id == contentId);
            if (content == null)
                throw new ContentNotFoundException();

            return content;
        }

        public async Task<List<Content>> GetAllContents(Guid userId, Guid courseId, int lessonId)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            var contents = userCourseLesson.Contents?.ToList();
            if (contents == null)
                throw new ContentNotFoundException();

            return contents;
        }
    }
}
