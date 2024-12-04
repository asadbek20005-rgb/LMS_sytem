using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Lesson
{
    public interface ILessonIntegration
    {
        Task<Tuple<HttpStatusCode, List<LessonDto>>> GetAllOwnerLessons(Guid courseId);
        Task<Tuple<HttpStatusCode, LessonDto>> GetOwnerLesson(Guid courseId, int lessonId);
        Task<Tuple<HttpStatusCode, List<LessonDto>>> GetAllClientLessons(Guid courseId);
        Task<Tuple<HttpStatusCode, LessonDto>> GetClientLesson(Guid courseId, int lessonId);
        Task<HttpStatusCode> AddOwnerLesson(Guid courseId, CreateLessonModel createLessonModel);
    }
}
