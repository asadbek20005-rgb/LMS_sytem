using LMS.Common.Models;
using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Exceptions.Content;
using LMS.Data.Repositories.Interfaces;

namespace LMS.Data.Repositories.Implementations
{
    public class ContentRepository(AppDbContext appDbContext, ILessonRepository lessonRepository) : IContentRepository
    {
        private readonly AppDbContext _context = appDbContext;
        private readonly ILessonRepository _lessonRepository = lessonRepository;
   


       

        public async Task<Content> GetContentById(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            var content = (userCourseLesson.Contents?.SingleOrDefault(x => x.Id == contentId)) ?? throw new ContentNotFoundException();
            return content;
        }

        public async Task<List<Content>> GetAllContents(Guid userId, Guid courseId, int lessonId)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            var contents = (userCourseLesson.Contents?.ToList()) ?? throw new ContentNotFoundException();
            return contents;
        }

        public async Task AddOrUpdateContent(Content content)
        {
            await _context.AddAsync(content);
            await _context.SaveChangesAsync();
        }
    }
}
