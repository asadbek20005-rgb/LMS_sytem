using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Exceptions.User;
using LMS.Data.Exceptions.User_Course;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class CourseService(ICourseRepository courseRepository, IUser_CourseRepository user_CourseRepository, IUserRepository userRepository)
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUser_CourseRepository _userCourseRepository = user_CourseRepository;

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllCourses();
            return courses.ParseToDtos();
        }


        public async Task<List<CourseDto>> SortBy(string? category = null, string? title = null, decimal? price = null)
        {
            try
            {
                var courses = await _courseRepository.SearchUserCourseBy(category, title, price);
                return courses.ParseToDtos();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CourseDto>> SortByCategory(string category)
        {
            try
            {

                var courses = await _courseRepository.SeachUserCourseByCategory(category);
                return courses.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseDto> AddCourse(Guid userId, CreateCourseModel createCourseModel)
        {
            try
            {

                await CheckUserExist(userId);
                await CheckCourse(createCourseModel.Title);

                var newCourse = new Course
                {
                    Title = createCourseModel.Title,
                    Description = createCourseModel.Description,
                    Price = createCourseModel.Price,
                    Category = createCourseModel.Category
                };

                await _courseRepository.CreateCourse(newCourse);

                var newUserCourse = new User_Course
                {
                    UserId = userId,
                    CourseId = newCourse.Id,
                    Course = newCourse,
                    IsPayed = false,
                    IsOwner = true,
                    IsFree = false
                } ?? throw new User_Course_PaymentNotFoundException();
                await _userCourseRepository.AddUserCourse(newUserCourse);
                return newCourse.ParseToDto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseDto> AddFreeCourse(Guid userId, CreateFreeCourseModel createFreeCourseModel)
        {

            try
            {

                await CheckUserExist(userId);
                await CheckCourse(createFreeCourseModel.Title);

                var newFreeCourse = new Course
                {
                    Title = createFreeCourseModel.Title,
                    Description = createFreeCourseModel.Description,
                    Category = createFreeCourseModel.Category,
                    Price = createFreeCourseModel.Price
                };

                await _courseRepository.CreateCourse(newFreeCourse);

                var newFreeUserCourse = new User_Course
                {
                    UserId = userId,
                    CourseId = newFreeCourse.Id,
                    Course = newFreeCourse,
                    IsFree = true,
                    IsOwner = true,
                    IsPayed = false
                };

                await _userCourseRepository.AddUserCourse(newFreeUserCourse);

                return newFreeCourse.ParseToDto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<CourseDto>> GetAllClientCourses(Guid userId)
        {
            try
            {

                var allCourses = await _courseRepository.GetAllPayedUserCourses(userId);
                return allCourses.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CourseDto>> GetAllUserCourses(Guid userId)
        {
            try
            {

                var userCourses = await _courseRepository.GetAllUserCourses(userId);
                return userCourses.ParseToDtos();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseDto> GetUserCourseById(Guid userId, Guid courseId)
        {
            try
            {

                var course = await _courseRepository.GetUserCourseById(userId, courseId);
                return course.ParseToDto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseDto> UpdateUserCoursePrice(Guid userId, Guid courseId, UpdateCourseModel updateCourseModel)
        {
            try
            {
                var course = await _courseRepository.GetUserCourseById(userId, courseId);
                course.Price = updateCourseModel.Price;
                await _courseRepository.UpdateCourse(course);
                return course.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<CourseDto> UpdateUserCourseTitle(Guid userId, Guid courseId, UpdateCourseModel updateCourseModel)
        {
            try
            {

                var userCourse = await _courseRepository.GetUserCourseById(userId, courseId);
                if (!string.IsNullOrWhiteSpace(updateCourseModel.Title))
                {
                    userCourse.Title = updateCourseModel.Title;
                }
                await _courseRepository.UpdateCourse(userCourse);
                return userCourse.ParseToDto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseDto> GetCourseById(Guid courseId)
        {
            try
            {
                var course = await _courseRepository.GetCourseById(courseId);
                return course.ParseToDto();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task CheckUserExist(Guid userId)
        {
            _ = await _userRepository.GetUserById(userId) ?? throw new UserNotFoundException();
        }


        private async Task CheckCourse(string title)
        {

            await _courseRepository.CheckCourseFoTitle(title);
        }
    }
}