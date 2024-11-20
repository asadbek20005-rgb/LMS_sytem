using LMS.Common.Constants;
using LMS.Common.Models;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Client
{
    [Route("api/clients/clientId/[controller]")]
    [ApiController]
    public class CardsController(CardInfoService cardInfoService, UserHelper userHelper) : ControllerBase
    {
        private readonly CardInfoService _cardInfoService = cardInfoService;
        private readonly UserHelper _userHelper = userHelper;

        [HttpPost]
        [Authorize(Roles = Constants.Client)]
        public async Task<IActionResult> EnterCardInfo(CreateCardInfoModel createCardInfoModel)
        {
            var userId = _userHelper.GetUserId();
            var dto = await _cardInfoService.EnterCardInfo(userId, createCardInfoModel);
            return Ok(dto);
        }
    }
}
