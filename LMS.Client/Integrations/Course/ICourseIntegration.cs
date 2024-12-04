using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Course
{
    public interface ICourseIntegration
    {
       
        Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses();
        Task<HttpStatusCode> CreateOwnerCourse(CreateCourseModel createCourseModel);
        Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllOwnerCourses();
        Task<(HttpStatusCode, List<CourseDto>)> GetAllClientCourses();
    }
}
