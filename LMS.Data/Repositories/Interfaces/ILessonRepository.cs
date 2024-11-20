using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        Task CreateUserCourseLesson(Lesson lesson);
        Task<List<Lesson>> GetAllUserCourseLessons(Guid userId, Guid courseId);
        Task<Lesson> GetUserCourseLessonById(Guid userId, Guid courseId, int lessonId);
        Task UpdateUserCourseLesson(Lesson lesson);
        Task CheckLessonExistForTitle(string title);
    }
}
