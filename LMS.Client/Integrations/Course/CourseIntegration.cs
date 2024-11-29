using LMS.Client.Helper;
using LMS.Client.LocalStorage;
using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Course
{
    public class CourseIntegration(TokenHelper tokenHelper,HttpClient httpClient, LocalStorageService localStorageService) : ICourseIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly LocalStorageService _localStorageService = localStorageService;
        private readonly TokenHelper _tokenHelper = tokenHelper;

        public async Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses()
            {
            string url = "/api/Courses";
            var response = await _httpClient.GetAsync(url);
            var dtos = await response.Content.ReadFromJsonAsync<List<CourseDto>>();
            if (dtos == null)
                return new(HttpStatusCode.NotFound, new List<CourseDto>());
            return new(response.StatusCode, dtos);
        }

        public async Task<HttpStatusCode> CreateOwnerCourse(CreateCourseModel createCourseModel)
        {
            await _tokenHelper.AddTokenToHeader();
            string url = "/api/owners/ownerId/OwnerCourses";
            var response = await _httpClient.PostAsJsonAsync(url, createCourseModel);
            return response.StatusCode;
        }
        public async Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllOwnerCourses()
        {
            await _tokenHelper.AddTokenToHeader();
            string url = "/api/owners/ownerId/OwnerCourses";
            var response = await _httpClient.GetAsync(url);
            var dtos = await response.Content.ReadFromJsonAsync<List<CourseDto>>();
            if (dtos == null)
                return new(HttpStatusCode.NotFound, new List<CourseDto>());

            return new(response.StatusCode, dtos);

        }


   

    }
}