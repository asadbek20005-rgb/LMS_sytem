using LMS.Client.Helper;
using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Lesson
{
    public class LessonIntegration(HttpClient httpClient, TokenHelper tokenHelper) : ILessonIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenHelper _tokenHelper = tokenHelper;

        public async Task<HttpStatusCode> AddOwnerLesson(Guid courseId, CreateLessonModel createLessonModel)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/OwnerLessons";
            var response = await _httpClient.PostAsJsonAsync(url, createLessonModel);
            return response.StatusCode;
        }

        public async Task<Tuple<HttpStatusCode, List<LessonDto>>> GetAllClientLessons(Guid courseId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/ClientLessons";
            var response = await _httpClient.GetAsync(url);
            var lessonDtos = await response.Content.ReadFromJsonAsync<List<LessonDto>>();
            if (lessonDtos is null)
                return new(HttpStatusCode.NotFound, new List<LessonDto>());
            return new(response.StatusCode, lessonDtos);
        }

        public async Task<Tuple<HttpStatusCode, List<LessonDto>>> GetAllOwnerLessons(Guid courseId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/OwnerLessons";
            var response = await _httpClient.GetAsync(url);
            var lessonDtos = await response.Content.ReadFromJsonAsync<List<LessonDto>>();
            if (lessonDtos is null)
                return new(HttpStatusCode.NotFound, new List<LessonDto>());
            return new(response.StatusCode, lessonDtos);
        }

        public async Task<Tuple<HttpStatusCode, LessonDto>> GetClientLesson(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/clients/clientId/courses/{courseId}/ClientLessons/{lessonId}";
            var response = await _httpClient.GetAsync(url);
            var lessonDto = await response.Content.ReadFromJsonAsync<LessonDto>();
            if (lessonDto is null)
                return new(HttpStatusCode.NotFound, lessonDto);
            return new(response.StatusCode, lessonDto); 
        }

        public async Task<Tuple<HttpStatusCode, LessonDto>> GetOwnerLesson(Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/owners/ownerId/courses/{courseId}/OwnerLessons/{lessonId}";
            var response = await _httpClient.GetAsync(url);
            var lessonDto = await response.Content.ReadFromJsonAsync<LessonDto>();
            if(lessonDto is null)
                return new(HttpStatusCode.NotFound, lessonDto);
            return new(response.StatusCode, lessonDto);
        }
    }
}
