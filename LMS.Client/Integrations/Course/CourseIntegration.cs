using LMS.Common.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace LMS.Client.Integrations.Course
{
    public class CourseIntegration(HttpClient httpClient) : ICourseIntegration
    {
        private readonly HttpClient _httpClient = httpClient;


        public async Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses()
        {
            string url = "/api/Courses";
            var response = await _httpClient.GetAsync(url);
            var dtos = await response.Content.ReadFromJsonAsync<List<CourseDto>>();
            if (dtos == null)
                throw new Exception();
            return new (response.StatusCode, dtos);
        }
    }
}
