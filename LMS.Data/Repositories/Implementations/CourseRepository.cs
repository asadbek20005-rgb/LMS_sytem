using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.Course;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class CourseRepository(AppDbContext appDbContext) : ICourseRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CheckCourseFoTitle(string title)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Title == title);
            var checkingForTitle = course != null;
            if (checkingForTitle)
                throw new SameCourseException($"Course with title <<{title}>> is already exist");
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
            var userCourse = _context.User_Courses
                .AsNoTracking()
                .Include(x => x.Course);          
                var res = await userCourse.Where(x => x.UserId == userId && x.CourseId == courseId)
                  .Select(uc => uc.Course)
                  .FirstOrDefaultAsync() ?? throw new CourseNotFoundException();
                return res;
        }

        public async Task<List<Course>> SeachUserCourseByCategory(string category)
        {
            var userCourses = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                userCourses = userCourses.Where(x => x.Category.Contains(category));
            }

            return await userCourses.ToListAsync();
        }

        public async Task<List<Course>> SearchUserCourseBy(string? category = null, string? title = null, decimal? price = null)
        {
            var userCourses = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                userCourses = userCourses.Where(x => x.Category.Contains(category));
            }

            if (!string.IsNullOrEmpty(title))
            {
                userCourses = userCourses.Where(x => x.Title.Contains(title));
            }

            if (price.HasValue)
            {
                userCourses = userCourses.Where(x => x.Price == price);
            }

            return await userCourses.ToListAsync();
        }






        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
