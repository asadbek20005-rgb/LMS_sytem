using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions.User_Course;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class User_Course_PaymentRepository(AppDbContext appDbContext) : IUser_Course_PaymentRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CheckCoursePayment(Guid userId, Guid courseId)
        {
            var userCourse = await _context.User_Courses.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
            if (userCourse == null)
                throw new User_Course_NotFoundException();

            var isPayed = userCourse.IsPayed == true;

            if (isPayed)
                throw new Exception("The Course is already payed");
        }

        public async Task CreateUser_Course_Payment(User_Course_Payment user_Course_Payment)
        {
            await _context.User_Course_Payments.AddAsync(user_Course_Payment);
            await _context.SaveChangesAsync();
        }


    }
}
