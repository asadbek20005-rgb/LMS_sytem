using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using LMS.Service.Helpers;

namespace LMS.Service.Api
{
    public class User_Course_PaymentService(IUserRepository userRepository, IUser_CourseRepository user_CourseRepository, IUser_Course_PaymentRepository user_Course_PaymentRepository, ICourseRepository courseRepository)
    {
        private readonly IUser_Course_PaymentRepository _repository = user_Course_PaymentRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IUser_CourseRepository _userCourseRepository = user_CourseRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User_Course_PaymentDto> PayPayment(Guid userId, Guid courseId, CreateUser_Course_Payment createUser_Course_Payment)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                var userCourse = await _userCourseRepository.GetCourseById(courseId);
                var course = await _courseRepository.GetCourseById(userCourse.CourseId);
                if (course.Price != createUser_Course_Payment.Amount)
                    throw new Exception("Amuount is not correct");
                decimal totalAmount = await PaymentHelperProseccer.CalculateAmount(createUser_Course_Payment.Amount);

                var newPayment = new User_Course_Payment
                {
                    UserId = userId,
                    CourseId = courseId,
                    Amount = totalAmount,
                    Status = createUser_Course_Payment.Status,
                };
                var newUserCourse = new User_Course
                {
                    UserId = userId,
                    CourseId = course.Id,
                    IsPayed = true,
                };


                await _repository.CreateUser_Course_Payment(newPayment);
                await _userCourseRepository.AddUserCourse(newUserCourse);
                return newPayment.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
