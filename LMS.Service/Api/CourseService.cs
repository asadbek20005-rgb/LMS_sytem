using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<CourseDto> AddCourse(Guid userId, CreateCourseModel createCourseModel)
        {
            await CheckUser(userId);
            var newCourse = new Course
            {
                Title = createCourseModel.Title,
                Description = createCourseModel.Description,
                Price = createCourseModel.Price,
                Category = createCourseModel.Category
            };
            await _courseRepository.CreateCourse(newCourse);
            return newCourse.ParseToDto();
        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var allCourses = await _courseRepository.GetAllCourses();
            return allCourses.ParseToDtos();
        }

        public async Task<List<CourseDto>> GetAllUserCourses(Guid userId)
        {
            var userCourses = await _courseRepository.GetAllUserCourses(userId);
            return userCourses.ParseToDtos();
        }

        public async Task<CourseDto> GetUserCourseById(Guid userId, Guid courseId)
        {
            var course = await _courseRepository.GetUserCourseById(userId, courseId);
            return course.ParseToDto();
        }

        public async Task<CourseDto> UpdateUserCoursePrice(Guid userId, Guid courseId, UpdateCourseModel updateCourseModel)
        {
            var course = await _courseRepository.GetUserCourseById(userId, courseId);
            course.Price = updateCourseModel.Price;
            await _courseRepository.UpdateCourse(course);
            return course.ParseToDto();
        }

      

        public async Task<CourseDto> UpdateUserCourseTitle(Guid userId, Guid courseId, UpdateCourseModel updateCourseModel)
        {
            var userCourse = await _courseRepository.GetUserCourseById(userId, courseId);
            if (!string.IsNullOrWhiteSpace(updateCourseModel.Title))
            {
                userCourse.Title = updateCourseModel.Title;
            }
            await _courseRepository.UpdateCourse(userCourse);
            return userCourse.ParseToDto();
        }
        private async Task CheckUser(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                throw new UserNotFoundException();
        }
    }
}
