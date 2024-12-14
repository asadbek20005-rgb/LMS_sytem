using LMS.Client.Helper;
using LMS.Client.LocalStorage;
using LMS.Common.Dtos;
using LMS.Common.Models.AccountModels;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Admin
{
    public class AdminIntegration(HttpClient httpClient, TokenHelper tokenHelper, LocalStorageService localStorageService) : IAdminIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly TokenHelper _tokenHelper = tokenHelper;
        private readonly LocalStorageService _localStorageService = localStorageService;
        public async Task<HttpStatusCode> BlockUser(Guid userId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/block?userId={userId}";
            HttpContent content = new StringContent(string.Empty);

            var response = await _httpClient.PutAsync(url, content);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteContent(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/content?userId={userId}&courseId={courseId}&lessonId={lessonId}&contentId={contentId}";
            var response = await _httpClient.DeleteAsync(url);
            return response.StatusCode;
        }



        public async Task<(HttpStatusCode, List<CourseDto>)> GetAllCourses()
        {
            await _tokenHelper.AddTokenToHeader();
            string url = "/api/Admins/action/courses";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var courseDtos = await response.Content.ReadAsAsync<List<CourseDto>>();

                return (response.StatusCode, courseDtos);
            }
            else
            {
                return (HttpStatusCode.NotFound, null);
            }
        }

        public Task<(HttpStatusCode, List<LessonDto>)> GetAllLessons()
        {
            throw new NotImplementedException();
        }

        public async Task<(HttpStatusCode, List<UserDto>)> GetAllUsers()
        {
            await _tokenHelper.AddTokenToHeader();
            string url = "/api/Admins/action/users";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var userDtos = await response.Content.ReadAsAsync<List<UserDto>>();

                return (response.StatusCode, userDtos);
            }
            else
            {
                return (HttpStatusCode.NotFound, null);
            }
        }

        public async Task<(HttpStatusCode, List<LessonDto>)> GetCourseLessons(Guid userId, Guid courseId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/lessons?userId={userId}&courseId={courseId}";
            var response = await _httpClient.GetAsync(url);
            var lessonDtos = await response.Content.ReadFromJsonAsync<List<LessonDto>>();
            return (response.StatusCode, lessonDtos);   
        }

        public async Task<(HttpStatusCode, LessonDto)> GetCourseLesson(Guid userId, Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/lesson?userId={userId}&courseId={courseId}&lessonId={lessonId}";
            var response = await _httpClient.GetAsync(url);
            var lessonDto = await response.Content.ReadFromJsonAsync<LessonDto>();  
            return (response.StatusCode, lessonDto);

        }

        public async Task<(Stream, string, string)> GetLessonContent(Guid userId, Guid courseId, int lessonId, int contentId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/content?userId={userId}&courseId={courseId}&lessonId={lessonId}&contentId={contentId}";
            var response = await _httpClient.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var contentDisposition =  response.Content.Headers.ContentDisposition;
                var contentType = response.Content.Headers.ContentType?.MediaType?? "application/octet-stream";
                var fileName = contentDisposition?.FileName ?? string.Empty;

                return (contentStream, fileName, contentType);
            }
            return (null, null, null);
        }

        public async Task<(HttpStatusCode, List<ContentDto>)> GetLessonContents(Guid userId, Guid courseId, int lessonId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/contents?userId={userId}&courseId={courseId}&lessonId={lessonId}";
            var response = await _httpClient.GetAsync(url);
            var contentDtos = await response.Content.ReadFromJsonAsync<List<ContentDto>>();
            return (response.StatusCode, contentDtos);

        }

        public async Task<(HttpStatusCode, CourseDto)> GetUserCourse(Guid userId, Guid courseId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/user-course?userId={userId}&courseId={courseId}";
            var response = await _httpClient.GetAsync(url);
            var courseDto = await response.Content.ReadAsAsync<CourseDto>();
            return (response.StatusCode, courseDto);
        }

        public async Task<(HttpStatusCode, List<CourseDto>)> GetUserCourses(Guid userId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/user-courses?userId={userId}";
            var response = await _httpClient.GetAsync(url);
            var courseDtos = await response.Content.ReadFromJsonAsync<List<CourseDto>>();
            return (response.StatusCode, courseDtos);
        }

        public async Task<HttpStatusCode> Login(AdminLoginModel model)
        {
            string url = "/api/Admins/action/login";
            var response = await _httpClient.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorageService.SetToken(token);
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> UnBlockUser(Guid userId)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = $"/api/Admins/action/unblock?userId={userId}";
            HttpContent content = new StringContent(string.Empty);
            var response = await _httpClient.PutAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
