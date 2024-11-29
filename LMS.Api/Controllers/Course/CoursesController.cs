using LMS.Service.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Course
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(CourseService courseService) : ControllerBase
    {
        private readonly CourseService _courseService = courseService;


        //[HttpGet]
        //public async Task<IActionResult> GetAllCourses()
        //{
        //    var courses = await _courseService.get();
        //    return Ok(courses);
        //}
        [HttpGet("{courseId:guid}")]
        public async Task<IActionResult> GetCourse(Guid courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            return Ok(course);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortBy([FromQuery] string? category = null, [FromQuery] string? title = null, [FromQuery] decimal? price = null)
        {
            var courses = await _courseService.SortBy(category, title, price);
            return Ok(courses);
        }
    }
}
