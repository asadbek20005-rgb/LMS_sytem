using LMS.Common.Constants;
using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;

namespace LMS.Data.Repositories.Implementations
{
    public class User_Course_PaymentRepository(AppDbContext appDbContext) : IUser_Course_PaymentRepository
    {
        private readonly AppDbContext _context = appDbContext;

     
        public async Task CreateUser_Course_Payment(User_Course_Payment user_Course_Payment)
        {
            await _context.User_Course_Payments.AddAsync(user_Course_Payment);
            await _context.SaveChangesAsync();
        }

      
    }
}
