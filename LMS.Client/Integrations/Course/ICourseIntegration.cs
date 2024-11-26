﻿using LMS.Common.Dtos;
using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Course
{
    public interface ICourseIntegration
    {
       
        Task<Tuple<HttpStatusCode, List<CourseDto>>> GetAllCourses();
    }
}
