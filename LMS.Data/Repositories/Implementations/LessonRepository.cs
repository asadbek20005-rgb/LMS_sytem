﻿using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.Lesson;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class LessonRepository(AppDbContext context) : ILessonRepository
    {
        private readonly AppDbContext _context = context;

        public async Task CheckLessonExistForTitle(string title)
        {
            var lesson = await _context.Lessons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title);
            if (lesson != null)
                throw new SameLessonExistException($"Lesson with {title} is already exist");
        }

        public async Task CreateUserCourseLesson(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }



        public async Task<List<Lesson>> GetAllUserCourseLessons(Guid userId, Guid courseId)
        {
            var userCourse = await _context.User_Courses
                .AsNoTracking()
                .Include(uc => uc.Course)
                    .ThenInclude(c => c.Lessons)
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (userCourse == null)
            {
                throw new Exceptions.User_Course.User_Course_NotFoundException();
            }

            var userCourseLessons = userCourse.Course?.Lessons?.ToList() ?? throw new Exceptions.Lesson.LessonNotFoundException();
            return userCourseLessons;

        }

        public async Task<Lesson> GetUserCourseLessonById(Guid userId, Guid courseId, int lessonId)
        {
            var userCourse = await _context.User_Courses
                .Include(uc => uc.Course)
                .ThenInclude(c => c.Lessons) 
                .ThenInclude(c => c.Contents)
                .Where(uc => uc.UserId == userId && uc.CourseId == courseId)
                .FirstOrDefaultAsync();

            if (userCourse is null)
                throw new Exception("User_Course not found.");

            var userCourseLesson = userCourse.Course?.Lessons?
                .FirstOrDefault(l => l.Id == lessonId);

          
           
            if (userCourseLesson is null)
                throw new LessonNotFoundException();

            return userCourseLesson;
        }


        public async Task UpdateUserCourseLesson(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            await _context.SaveChangesAsync();

        }
    }
}
