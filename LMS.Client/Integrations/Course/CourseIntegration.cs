using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Course
{
    public class CourseIntegration(HttpClient httpClient) : ICourseIntegration
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<HttpStatusCode> CreateCourse(CreateCourseModel model)
        {
            string url = "/api/Courses";
            var response = await _httpClient.PostAsJsonAsync(url, model);
            return response.StatusCode;
        }

        public async Task<Tuple<HttpStatusCode, CourseDto>> CreateFreeCourse(CreateFreeCourseModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses()
        {
            throw new NotImplementedException();
        }
    }
}
