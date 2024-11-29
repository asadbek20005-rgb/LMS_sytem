using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IUser_CourseRepository
    {
        Task AddUserCourse(Data.Entities.User_Course user_Course);
        Task<User_Course> GetUserCourseById(Guid userId,Guid courseId);
        Task<User_Course> GetCourseById(Guid courseId);
        Task UpdateUserCourse(User_Course user_Course);
    }
}
