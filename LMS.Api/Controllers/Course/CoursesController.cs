using LMS.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Course
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(CourseService courseService) : ControllerBase
    {
        private readonly CourseService courseService = courseService;

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await courseService.GetAllCourses();  
            return Ok(courses);
        }
    }
}
