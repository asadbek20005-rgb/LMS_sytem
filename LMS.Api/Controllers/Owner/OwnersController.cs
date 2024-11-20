using LMS.Common.Models.AccountModels;
using LMS.Common.OTPModels;
using LMS.Service.Api;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Owner
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController(OwnerService ownerService) : ControllerBase
    {

        private readonly OwnerService _ownerService = ownerService;

        [HttpPost("account/register")]
        public async Task<IActionResult> Register(OwnerRegisterModel userRegisterModel)
        {
            try
            {
                var code = await _ownerService.Register(userRegisterModel);
                return Ok(code);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("account/verify-register")]
        public async Task<IActionResult> VerifyRegister(OtpModel otpModel)
        {
            string result = await _ownerService.VerifyRegister(otpModel);
            return Ok(result);
        }



        [HttpPost("account/login")]
        public async Task<IActionResult> Login(OwnerLoginModel userLoginModel)
        {
            int code = await _ownerService.Login(userLoginModel);
            if (ModelState.IsValid)
            {
                return Ok(code);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("account/verify-login")]
        public async Task<IActionResult> VerifyLogin(OtpModel otpModel)
        {
            string token = await _ownerService.VerifyLogin(otpModel);
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
