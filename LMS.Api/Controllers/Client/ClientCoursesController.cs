using LMS.Common.Constants;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/[controller]")]
    [ApiController]
    public class ClientCoursesController(CourseService courseService, UserHelper userHelper) : ControllerBase
    {
        private readonly CourseService _courseService = courseService;
        private readonly UserHelper _userHelper = userHelper;

        [HttpGet()]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> GetAllClientCourses()
        {
            var clientId = _userHelper.GetUserId();
            var dtos = await _courseService.GetAllClientCourses(clientId);
            return Ok(dtos);
        }

        [HttpGet("{courseId:guid}")]
        [Authorize(Roles =Constants.Client)]
        public async Task<IActionResult> GetClientCourseById(Guid courseId)
        {
            var userId = _userHelper.GetUserId();
            var course = await _courseService.GetUserCourseById(userId, courseId);
            return Ok(course);
        }


      

    }
}