using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IUser_Course_PaymentRepository
    {
        Task CreateUser_Course_Payment(User_Course_Payment user_Course_Payment);
        Task CheckCoursePayment(Guid userId, Guid courseId);
    }
}
