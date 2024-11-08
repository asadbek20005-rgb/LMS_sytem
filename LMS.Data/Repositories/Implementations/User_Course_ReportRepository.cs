using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class User_Course_ReportRepository : IUser_Course_Report
    {
        private readonly AppDbContext _context;
        public User_Course_ReportRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task CreateUser_Course_Report(User_Course_Report user_Course_Report)
        {
            await _context.User_Course_Reports.AddAsync(user_Course_Report);
            await _context.SaveChangesAsync();
        }

        public Task DeleteUser_Course_Report(User_Course_Report user_Course_Report)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User_Course_Report>> GetAllUser_Course_Repos()
        {
            return await _context.User_Course_Reports.ToListAsync();
        }

        public async Task<User_Course_Report> GetUser_Course_ReportByCourseId(Guid courseId)
        {
            var userCourseReport = await _context.User_Course_Reports.FindAsync(courseId);
            if (userCourseReport == null)
                throw new User_Course_ReportNotFoundException();
            return userCourseReport;
        }

        public async Task<User_Course_Report> GetUser_Course_ReportByUserId(Guid userId)
        {
            var userCourseReport = await _context.User_Course_Reports.FindAsync(userId);
            if (userCourseReport == null)
                throw new User_Course_ReportNotFoundException();
            return userCourseReport;
        }

        public async Task UpdateUser_Course_Report(User_Course_Report user_Course_Report)
        {
            _context.User_Course_Reports.Update(user_Course_Report);
            await _context.SaveChangesAsync();
        }
    }
}
