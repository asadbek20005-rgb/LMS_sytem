using LMS.Common.Constants;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/courses/{courseId:guid}/lessons/{lessonId:int}[controller]")]
    [ApiController]
    public class ClientContentsController(ContentServce contentServce, UserHelper userHelper) : ControllerBase
    {
        private readonly ContentServce _contentServce = contentServce;
        private readonly UserHelper _userHelper = userHelper;   

        [HttpGet]
        [Authorize(Roles =Constants.Client)]
        public async Task<IActionResult> GetLessonContents(Guid courseId, int lessonId)
        {
            var userId = _userHelper.GetUserId();
            var contents = await _contentServce.GetAllContents(userId,courseId, lessonId); 
            return Ok(contents);
        }

        [HttpGet("{contentId:int}")]
        [Authorize(Roles =Constants.Client)]
        public async Task<IActionResult> GetLessonContentById(Guid courseId, int lessonId, int contentId)
        {
            var userId = _userHelper.GetUserId();
            var (stream, fileName, contentType) = await _contentServce.GetContent(userId,courseId, lessonId, contentId);
            return File(stream, fileName, contentType);
        }
    }
}
