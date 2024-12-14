using LMS.Common.Constants;
using LMS.Common.Models.AccountModels;
using LMS.Service.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController(AdminService adminService) : ControllerBase
    {
        private readonly AdminService _adminService = adminService;

        [HttpPost("action/login")]
        public async Task<IActionResult> Login([FromBody] AdminLoginModel adminLoginModel)
        {
            string token = await _adminService.Login(adminLoginModel);
            return Ok(token);
        }

        [HttpGet("action/users")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("action/courses")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _adminService.GetAllCourses();
            return Ok(courses);
        }

        [HttpGet("action/user-courses")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetUserCourses([FromQuery]Guid userId)
        {
            var userCourses = await _adminService.GetUserCourses(userId);
            return Ok(userCourses); 
        }


        [HttpGet("action/user-course")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetUserCourse([FromQuery]Guid userId, [FromQuery]Guid courseId)
        {
            var userCourse = await _adminService.GetUserCourse(userId,  courseId);
            return Ok(userCourse);  
        }


        [HttpGet("action/lessons")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetCourseLessons([FromQuery] Guid userId,[FromQuery] Guid courseId)
        {
            var lessons = await _adminService.GetCourseLessons(userId, courseId);
            return Ok(lessons);
        }

        [HttpGet("action/lesson")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetCourseLesson([FromQuery]Guid userId, [FromQuery] Guid courseId, [FromQuery]int lessonId)
        {
            var courseLesson = await _adminService.GetCourseLesson(userId, courseId, lessonId);
            return Ok(courseLesson);
        }

        [HttpGet("action/contents")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetLessonContents([FromQuery] Guid userId, [FromQuery] Guid courseId,[FromQuery] int lessonId)
        {
            var lessonContents = await _adminService.GetLessonContents(userId, courseId, lessonId);
            return Ok(lessonContents);
        }

        [HttpGet("action/content")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> GetLessonContent([FromQuery] Guid userId, [FromQuery] Guid courseId, [FromQuery] int lessonId, [FromQuery] int contentId)
        {
            var content = await _adminService.GetLessonContent(userId, courseId, lessonId, contentId);
            return Ok(content);
        }

        [HttpPut("action/block")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> BlockUser([FromQuery] Guid userId)
        {
            await _adminService.BlockUser(userId);
            return Ok();
        }

        [HttpPut("action/unblock")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> UnBlockUser([FromQuery] Guid userId)
        {
            await _adminService.UnBlockUser(userId);
            return Ok();
        }


        [HttpDelete("action/content")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteContent([FromQuery] Guid userId, [FromQuery] Guid courseId, [FromQuery] int lessonId, [FromQuery] int contentId)
        {
            bool result = await _adminService.DeleteFile(userId, courseId, lessonId, contentId);
            return Ok(result);
        }



    }
}
