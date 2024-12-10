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
        public async Task<IActionResult> Login(AdminLoginModel adminLoginModel)
        { 
            string token = await _adminService.Login(adminLoginModel);
            return Ok(token);
        }

        [HttpGet("action/users")]
        [Authorize(Roles =Constants.Admin)]
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

        [HttpPut("action/block")]
        [Authorize(Roles = Constants.Admin)]

        public async Task<IActionResult> BlockUser(Guid userId)
        {
            await _adminService.BlockUser(userId);
            return Ok();
        }

        [HttpPut("action/unblock")]
        [Authorize(Roles = Constants.Admin)]
        public async Task<IActionResult> UnBlockUser(Guid userId)
        {
            await _adminService.UnBlockUser(userId);
            return Ok();
        }


        [HttpDelete("action/content")]
        [Authorize(Roles = Constants.Admin)]    
        public async Task<IActionResult> DeleteContent(Guid userId, Guid courseId, int lessonId, int contentId)
        {
           bool result = await _adminService.DeleteFile(userId,courseId,lessonId,contentId);
            return Ok(result);
        }

 

    }
}
