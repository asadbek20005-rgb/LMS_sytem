using LMS.Common.Constants;
using LMS.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> AddCourse(CreateCourseModel createCourseModel)
        {

        }
    }
}
