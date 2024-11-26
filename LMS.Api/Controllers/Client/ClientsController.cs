using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using LMS.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController(ClientService clientService) : ControllerBase
    {
        private readonly ClientService _clientService = clientService;

        [HttpPost("account/register")]
        public async Task<IActionResult> Register(ClientRegisterModel userRegisterModel)
        {
          int code =  await _clientService.Register(userRegisterModel);
            return Ok(code);
        }
        [HttpPost("account/verify-register")]
        public async Task<IActionResult> VerifyRegister(OtpModel otpModel)
        {
            string result = await _clientService.VerifyRegister(otpModel);
            return Ok(result);
        }
        [HttpPost("account/login")]
        public async Task<IActionResult> Login(ClientLoginModel userLoginModel)
        {
            var token = await _clientService.Login(userLoginModel);
            return Ok(token);
        }

        [HttpPost("account/verify-login")]
        public async Task<IActionResult> VerifyLogin(OtpModel otpModel)
        {
            string token = await _clientService.VerifyLogin(otpModel);
            if (ModelState.IsValid)
            {
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }






        //[HttpGet("/api/clients/courses")]
        //[Authorize(Roles = Constants.Client)]
        //public async Task<IActionResult> SortByCategory([FromQuery] string category )
        //{
        //    var courses = await _courseService.SortByCategory(category);
        //    return Ok(courses);
        //}

    }
}
