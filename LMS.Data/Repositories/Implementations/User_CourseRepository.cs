using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;

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
    }
}
