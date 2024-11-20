using LMS.Common.Constants;
using LMS.Common.Models;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/courses/courseId/[controller]")]
    [ApiController]
    public class PaymentsController(User_Course_PaymentService user_Course_PaymentService, UserHelper userHelper) : ControllerBase
    {
        private readonly User_Course_PaymentService _paymentService = user_Course_PaymentService;
        private readonly UserHelper _userHelper = userHelper;
        [HttpPost]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> MakePayment(Guid courseId, CreateUser_Course_Payment createUser_Course_Payment)
        {
            var userId = _userHelper.GetUserId();
            var dto = await _paymentService.PayPayment(userId, courseId, createUser_Course_Payment);
            return Ok(dto);
        }
    }
}