﻿using LMS.Common.Constants;
using LMS.Common.Models;
using LMS.Service.Api;
using LMS.Service.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers.Owner
{
    [Route("api/owners/ownerId/[controller]")]
    [ApiController]
    public class OwnerCoursesController(CourseService courseService, UserHelper userHelper) : ControllerBase
    {
        private readonly UserHelper _userHelper = userHelper;
        private readonly CourseService _courseService = courseService;

        [HttpPost]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> AddCourse(CreateCourseModel createCourseModel)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var dto = await _courseService.AddCourse(userId, createCourseModel);
                return Ok(dto);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("free-course")]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> AddFreeCourse(CreateFreeCourseModel createFreeCourseModel)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var dto = await _courseService.AddFreeCourse(userId, createFreeCourseModel);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> GetAllUserCourse()
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var allUserCourses = await _courseService.GetAllUserCourses(userId);
                return Ok(allUserCourses);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("set-price")]
        [Authorize(Roles = Constants.Owner)]
        public async Task<IActionResult> UpdateCoursePrice(Guid courseId, UpdateCourseModel updateCourseModel)
        {
            try
            {
                var userId = _userHelper.GetUserId();
                var dto = await _courseService.UpdateUserCoursePrice(userId, courseId, updateCourseModel);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}