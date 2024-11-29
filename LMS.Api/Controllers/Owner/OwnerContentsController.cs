﻿using LMS.Common.Constants;
using LMS.Common.Models;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Owner
{
    [Route("api/owners/ownerId/courses/courseId/lessons/lessonId/[controller]")]
    [ApiController]
    public class OwnerContentsController(ContentServce contentServce, UserHelper userHelper) : ControllerBase
    {
        private readonly ContentServce _contentServce = contentServce;
        private readonly UserHelper _userHelper = userHelper;

        [HttpPost]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> AddContent(Guid courseId, int lessonId, AddOrUpdateContentModel createContentModel)
        {
            var userId = _userHelper.GetUserId();
            var dto = await _contentServce.AddOrUpdateContent(userId, courseId, lessonId, createContentModel);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> GetContent(Guid courseId, int lessonId, int contentId)
        {
            var userId = _userHelper.GetUserId();
            var vedio = await _contentServce.GetContent(userId, courseId, lessonId, contentId);
            return Ok(vedio);
        }
    }
}