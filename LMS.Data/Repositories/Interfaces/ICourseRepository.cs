using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateCourse(Course course);
        Task<List<Course>> GetAllCourses();
        Task<List<Entities.Course>> GetAllUserCourses(Guid userId);
        Task<Course> GetUserCourseById(Guid userId,Guid courseId);
        Task<List<Course>> SearchUserCourseBy(string? category=null, string? title = null, decimal? price=null);
        Task<List<Course>> SeachUserCourseByCategory(string category);
        Task UpdateCourse(Course course);
        Task CheckCourseFoTitle(string title);  
    }
}
