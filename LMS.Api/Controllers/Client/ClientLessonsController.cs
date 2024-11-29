using LMS.Common.Constants;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/courses/{courseId:guid}/[controller]")]
    [ApiController]
    public class ClientLessonsController(LessonService lessonService, UserHelper userHelper) : ControllerBase
    {
        private readonly LessonService _lessonService = lessonService;
        private readonly UserHelper _userHelper = userHelper;


        [HttpGet]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> GetAllCourseLessons(Guid courseId)
        {
            var userId = _userHelper.GetUserId();
            var lessons = await _lessonService.GetAllUserCourseLessons(userId, courseId);
            return Ok(lessons);
        }

        [HttpGet("{lessonId:int}")]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> GetCourseLessonById(Guid courseId, int lessonId)
        {
            var userId = _userHelper.GetUserId();
            var lesson = await _lessonService.GetUserCourseLesson(userId, courseId, lessonId);
            return Ok(lesson);
        }
    }
}
