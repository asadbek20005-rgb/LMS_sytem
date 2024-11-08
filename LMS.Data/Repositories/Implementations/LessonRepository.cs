using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _context;
        public LessonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateUserCourseLesson(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

       

        public async Task<List<Lesson>> GetAllUserCourseLessons(Guid userId, Guid courseId)
        {
            var userCourse = await _context.User_Courses
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (userCourse == null)
                throw new User_Course_NotFoundException();
            var userCourseLessons = userCourse.Course?.Lessons?.ToList();

            if (userCourseLessons is null)
                throw new LessonNotFoundException();

            return userCourseLessons;
        }

        public async Task<Lesson> GetUserCourseLessonById(Guid userId, Guid courseId, int lessonId)
        {
            var userCourse = await _context.User_Courses
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (userCourse == null)
                throw new User_Course_NotFoundException();
            var userCourseLesson = userCourse.Course?.Lessons?.SingleOrDefault(x => x.Id == lessonId);
            if (userCourseLesson is null)
                throw new LessonNotFoundException();
            return userCourseLesson;
        }

        public async Task UpdateUserCourseLesson(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();

        }
    }
}
