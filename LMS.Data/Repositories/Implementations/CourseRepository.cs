using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.Course;
using LMS.Data.Exceptions.User_Course;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class CourseRepository(AppDbContext appDbContext) : ICourseRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CheckCourseFoTitle(string title)
        {
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
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
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<List<Course>> GetAllPayedUserCourses(Guid userId)
        {

            var userCourses = await _context.User_Courses
                .Where(uc => uc.UserId == userId && uc.IsPayed == true)
                .Include(uc => uc.Course)  
                .ToListAsync();

            
            if (userCourses == null)
            {
                throw new User_Course_NotFoundException();
            }

            
            var courses = userCourses.Select(uc => uc.Course).ToList();
            
            return courses;
        }


        public async Task<List<Course>> GetAllUserCourses(Guid userId)
        {
            var userCourse = await _context.User_Courses
                .AsNoTracking()
                .Include(x => x.Course)
                .Where(x => x.UserId == userId && x.IsOwner == true)
                .ToListAsync();

            var courses = userCourse.Select(uc => uc.Course).ToList();
            if ( courses is null)
                throw new CourseNotFoundException();
            return courses!;
        }

        public async Task<Course> GetCourseById(Guid courseId)
        {
            var userCourse = await _context.User_Courses
                .AsNoTracking()  
                .Include(uc => uc.Course) 
                .FirstOrDefaultAsync(uc => uc.CourseId == courseId);  

            if (userCourse == null)
                throw new User_Course_NotFoundException();

            var course = userCourse.Course;

            if (course == null)
                throw new CourseNotFoundException();

            return course;
            }

        public Task<Course> GetPayedUserCourse(Guid userId, Guid courseId)
        {
            throw new NotImplementedException();
        }

        

        public async Task<Course> GetUserCourseById(Guid userId, Guid courseId)
        {
            var course = await _context.User_Courses
                .Where(uc => uc.UserId == userId && uc.CourseId == courseId)
                .AsNoTracking()
                .Include(uc => uc.Course)
                .AsNoTracking()
                .Select(uc => uc.Course)
                .FirstOrDefaultAsync();

            return course is null ? throw new CourseNotFoundException() : course;
        }

        


        public async Task<List<Course>> SeachUserCourseByCategory(string category)
        {
            var userCourses = _context.Courses.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                userCourses = userCourses.AsNoTracking().Where(x => x.Category.Contains(category));
            }

            return await userCourses.AsNoTracking().ToListAsync();
        }

        public async Task<List<Course>> SearchUserCourseBy(string? category = null, string? title = null, decimal? price = null)
        {
            var userCourses = _context.Courses.AsNoTracking().AsQueryable();

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

            return await userCourses.AsNoTracking().ToListAsync();
        }






        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
