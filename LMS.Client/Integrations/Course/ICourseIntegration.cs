using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Course
{
    public interface ICourseIntegration
    {
        Task<HttpStatusCode> CreateCourse(CreateCourseModel model);
        Task<Tuple<HttpStatusCode, CourseDto>> CreateFreeCourse(CreateFreeCourseModel model);
        //Task<Tuple<HttpStatusCode, List<CourseDto>>> GetOwnerCourses(Guid ownerId);
        Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses();
        //Task<Tuple<HttpStatusCode, CourseDto>> GetClientCourse(Guid courseId);
    }
}
