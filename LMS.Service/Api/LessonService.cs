using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class LessonService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILessonRepository _lessonRepository;
        public LessonService(ILessonRepository lessonRepository, IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _lessonRepository = lessonRepository;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        public async Task<LessonDto> AddUserCourseLesson(Guid userId, Guid courseId, CreateLessonModel createLessonModel)
        {
            await CheckUserCourse(userId, courseId);

            var newLesson = new Lesson
            {
                Title = createLessonModel.Title,
                Description = createLessonModel.Description
            };

            return newLesson.ParseToDto();
        }

        public async Task<List<LessonDto>> GetAllUserCourseLessons(Guid userId, Guid courseId)
        {
            var userCourseLessons = await _lessonRepository.GetAllUserCourseLessons(userId, courseId);
            return userCourseLessons.ParseToDtos();
        }

        public async Task<LessonDto> GetUserCourseLesson(Guid userId, Guid courseId, int lessonId)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            return userCourseLesson.ParseToDto();
        }


        public async Task<LessonDto> UpdateUserCourseLessonName(Guid userId, Guid courseId, int lessonId, string lessonTitle)
        {
            var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
            userCourseLesson.Title = lessonTitle;
            await _lessonRepository.UpdateUserCourseLesson(userCourseLesson);
            return userCourseLesson.ParseToDto();
        }

        private async Task CheckUserCourse(Guid userId, Guid courseId)
        {
            try
            {
                var userCourse = await _courseRepository.GetUserCourseById(userId, courseId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
