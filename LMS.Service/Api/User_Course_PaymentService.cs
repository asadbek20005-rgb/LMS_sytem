using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.Helpers;

namespace LMS.Service.Api
{
    public class User_Course_PaymentService(IUser_Course_PaymentRepository user_Course_PaymentRepository, ICourseRepository courseRepository)
    {
        private readonly IUser_Course_PaymentRepository _repository = user_Course_PaymentRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;

        public async Task<User_Course_PaymentDto> PayPayment(Guid userId, Guid courseId, CreateUser_Course_Payment createUser_Course_Payment)
        {
            try
            {
                _ = await _courseRepository.GetUserCourseById(userId, courseId);
                decimal totalAmount = await PaymentHelperProseccer.CalculateAmount(createUser_Course_Payment.Amount);

                var newPayment = new User_Course_Payment
                {
                    UserId = userId,
                    CourseId = courseId,
                    Amount = totalAmount,
                    Status = createUser_Course_Payment.Status,
                };

                await _repository.CreateUser_Course_Payment(newPayment);
                return newPayment.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
