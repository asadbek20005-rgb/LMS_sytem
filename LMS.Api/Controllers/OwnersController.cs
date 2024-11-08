using LMS.Common.Models;
using LMS.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly UserService _userService;

        public OwnersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("account/register")]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel)
        {
            try
            {
                await _userService.Register(userRegisterModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("account/login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
           string token = await _userService.Login(userLoginModel);
            if (ModelState.IsValid)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
