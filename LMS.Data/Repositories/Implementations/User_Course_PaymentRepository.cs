using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class User_Course_PaymentRepository : IUser_Course_PaymentRepository
    {
        private readonly AppDbContext _context;
        public User_Course_PaymentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task CreateUser_Course_Payment(User_Course_Payment user_Course_Payment)
        {
            await _context.User_Course_Payments.AddAsync(user_Course_Payment);
            await _context.SaveChangesAsync();
        }


        public async Task<List<User_Course_Payment>> GetAllUser_Course_Payments(Guid userId, Guid courseId)
        {
            var userCoursePayments = await _context.User_Course_Payments
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .ToListAsync();
            return userCoursePayments;
        }

        public async Task<User_Course_Payment> GetUser_Course_PaymentById(Guid userId, Guid courseId, Guid paymentId)
        {
            var userCoursePayment = await _context.User_Course_Payments
                 .Where(x => x.UserId == userId && x.CourseId == courseId && x.Id == paymentId)
                 .FirstOrDefaultAsync();
            if (userCoursePayment == null)
                throw new User_Course_PaymentNotFoundException();
            return userCoursePayment;
        }


        public async Task UpdateUser_Course_Payment(User_Course_Payment user_Course_Payment)
        {
            _context.User_Course_Payments.Update(user_Course_Payment);
            await _context.SaveChangesAsync();
        }
    }
}
