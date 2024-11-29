using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.User_Course;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class User_CourseRepository : IUser_CourseRepository
    {
        private readonly AppDbContext _context;
        public User_CourseRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddUserCourse(User_Course user_Course)
        {
            await _context.User_Courses.AddAsync(user_Course);
            await _context.SaveChangesAsync();
        }

        public async Task<User_Course> GetCourseById(Guid courseId)
        {
            var useCourse =await _context.User_Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (useCourse == null) 
                throw new User_Course_NotFoundException();
            return useCourse;
        }

        public async Task<User_Course> GetUserCourseById(Guid userId, Guid courseId)
        {
            var userCourse = await _context.User_Courses
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);

            if (userCourse == null)
                throw new User_Course_NotFoundException();

            return userCourse;

        }

        public async Task UpdateUserCourse(User_Course user_Course)
        {
            _context.User_Courses.Update(user_Course);
            await _context.SaveChangesAsync();
        }
    }
}
