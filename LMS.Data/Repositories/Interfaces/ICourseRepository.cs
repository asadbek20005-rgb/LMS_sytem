﻿using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateCourse(Course course);
        Task<List<Course>> GetAllCourses();
        Task<List<Entities.Course>> GetAllUserCourses(Guid userId);
        Task<Course> GetUserCourseById(Guid userId,Guid courseId);
        Task<List<Course>> SearchUserCourseBy(string? category, string? title, decimal? price);
        Task UpdateCourse(Course course);
    }
}
