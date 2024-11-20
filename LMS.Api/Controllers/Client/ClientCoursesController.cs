using LMS.Common.Constants;
using LMS.Service.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/[controller]")]
    [ApiController]
    public class ClientCoursesController(CourseService courseService) : ControllerBase
    {
        private readonly CourseService _courseService = courseService;

        [HttpGet()]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> GetAllCourses()
        {
            var dtos = await _courseService.GetAllCourses();
            return Ok(dtos);
        }

        [HttpGet("sort")]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> SortBy([FromQuery] string? category = null, [FromQuery] string? title = null, [FromQuery] decimal? price = null)
        {
            var courses = await _courseService.SortBy(category, title, price);
            return Ok(courses);
        }

    }
}