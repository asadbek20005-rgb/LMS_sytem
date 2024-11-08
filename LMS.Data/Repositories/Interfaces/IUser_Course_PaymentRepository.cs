using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IUser_Course_PaymentRepository
    {
        Task CreateUser_Course_Payment(User_Course_Payment user_Course_Payment);
        Task<List<User_Course_Payment>> GetAllUser_Course_Payments(Guid userId, Guid courseId);
        Task<User_Course_Payment> GetUser_Course_PaymentById(Guid userId, Guid courseId, Guid paymentId);
        Task UpdateUser_Course_Payment(User_Course_Payment user_Course_Payment);
    }
}
