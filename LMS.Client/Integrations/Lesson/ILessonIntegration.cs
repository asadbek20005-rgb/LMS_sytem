using LMS.Common.Dtos;
using System.Net;

namespace LMS.Client.Integrations.Lesson
{
    public interface ILessonIntegration
    {
        Task<Tuple<HttpStatusCode, List<LessonDto>>> GetAllCourseLessons();
        Task<Tuple<HttpStatusCode, LessonDto>> GetCourseLesson();

    }
}
