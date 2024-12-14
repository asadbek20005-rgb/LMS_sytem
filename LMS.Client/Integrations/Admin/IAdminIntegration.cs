using LMS.Common.Dtos;
using LMS.Common.Models.AccountModels;
using System.Net;

namespace LMS.Client.Integrations.Admin
{
    public interface IAdminIntegration
    {
        Task<HttpStatusCode> Login(AdminLoginModel model);
        Task<(HttpStatusCode, List<UserDto>)> GetAllUsers();
        Task<(HttpStatusCode, List<CourseDto>)> GetAllCourses();

        Task<(HttpStatusCode, List<CourseDto>)> GetUserCourses(Guid userId);
        Task<(HttpStatusCode, CourseDto)> GetUserCourse(Guid userId, Guid courseId);
        Task<(HttpStatusCode, List<LessonDto>)> GetCourseLessons(Guid userId, Guid courseId);
        Task<(HttpStatusCode, LessonDto)> GetCourseLesson(Guid userId, Guid courseId,int lessonId);
        Task<(HttpStatusCode, List<ContentDto>)> GetLessonContents(Guid userId, Guid courseId, int lessonId);
        Task<(Stream, string, string)> GetLessonContent(Guid userId, Guid courseId, int lessonId, int contentId);
        Task<HttpStatusCode> BlockUser(Guid userId);
        Task<HttpStatusCode> UnBlockUser(Guid userId);
        Task<HttpStatusCode> DeleteContent(Guid userId, Guid courseId, int lessonId, int contentId);

    }
}
