using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class User_Course_PaymentService
    {
        private readonly IUser_Course_PaymentRepository _repository;
        private readonly ICourseRepository _courseRepository;
        public User_Course_PaymentService(IUser_Course_PaymentRepository user_Course_PaymentRepository, ICourseRepository courseRepository)
        {
            _repository = user_Course_PaymentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<User_Course_PaymentDto> PayPayment(Guid userId, Guid courseId, CreateUser_Course_Payment createUser_Course_Payment)
        {
            var userCourse = await _courseRepository.GetUserCourseById(userId, courseId);

            var newPayment = new User_Course_Payment
            {
                UserId = userId,
                CourseId = courseId,
                Amount = createUser_Course_Payment.Amount,
                Status = createUser_Course_Payment.Status,
            };

            await _repository.CreateUser_Course_Payment(newPayment);
            return newPayment.ParseToDto();
        }


    }
}
