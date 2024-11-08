using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task CreateCourse(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<List<Course>> GetAllUserCourses(Guid userId)
        {
            var userCourse = await _context.User_Courses
                .Include(x => x.Course)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var courses = userCourse.Select(uc => uc.Course).ToList();
            if (courses.Count == 0 || courses is null)
                throw new CourseNotFoundException();
            return courses!;
        }

        public async Task<Course> GetUserCourseById(Guid userId, Guid courseId)
        {
            var userCourse = await _context.User_Courses
                .Include(x => x.Course)
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .Select(uc => uc.Course)
                .FirstOrDefaultAsync();

            if (userCourse == null)
                throw new CourseNotFoundException();
            return userCourse;
        }

        public async Task<List<Course>> SearchUserCourseBy(string? category, string? title, decimal? price)
        {
            var userCourses = _context.Courses.AsQueryable();
            userCourses = _context.Courses
               .Where(x => x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

            userCourses = _context.Courses
               .Where(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));


            userCourses = _context.Courses
               .Where(x => x.Price == price);

            return await userCourses.ToListAsync();
        }


        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
