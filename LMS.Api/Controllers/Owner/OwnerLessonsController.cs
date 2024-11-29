using LMS.Common.Constants;
using LMS.Common.Models;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Owner
{
    [Route("api/owners/ownerId/courses/courseId/[controller]")]
    [ApiController]
    public class OwnerLessonsController(LessonService lessonService, UserHelper userHelper) : ControllerBase
    {
        private readonly LessonService _lessonService = lessonService;
        private readonly UserHelper _userHelper = userHelper;

        [HttpPost]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> AddLesson(Guid courseId, CreateLessonModel createLessonModel)
        {
            var userId = _userHelper.GetUserId();
            var dto = await _lessonService.AddUserCourseLesson(userId, courseId, createLessonModel);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> GetAllLessons(Guid courseId)
        {
            var userId = _userHelper.GetUserId();
            var lessons = await _lessonService.GetAllUserCourseLessons(userId, courseId);
            return Ok(lessons);
        }

        [HttpGet("{lessonId:int}")]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> GetLessonById(Guid courseId, int lessonId)
        {
            var userId = _userHelper.GetUserId();
            var lesson = await _lessonService.GetUserCourseLesson(userId, courseId, lessonId);
            return Ok(lesson);
        }
    }
}
