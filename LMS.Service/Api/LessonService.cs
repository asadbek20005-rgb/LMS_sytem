using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class LessonService(ILessonRepository lessonRepository, ICourseRepository courseRepository)
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ILessonRepository _lessonRepository = lessonRepository;

        public async Task<LessonDto> AddUserCourseLesson(Guid userId, Guid courseId, CreateLessonModel createLessonModel)
        {
            try
            {
                await CheckUserCourseExist(userId, courseId);
                await CheckLessonTitle(createLessonModel.Title);

                var newLesson = new Lesson
                {
                    Title = createLessonModel.Title,
                    Description = createLessonModel.Description,
                    CourseId = courseId,
                };

                await _lessonRepository.CreateUserCourseLesson(newLesson);
                return newLesson.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LessonDto>> GetAllUserCourseLessons(Guid userId, Guid courseId)
        {
            try
            {
                var userCourseLessons = await _lessonRepository.GetAllUserCourseLessons(userId, courseId);
                return userCourseLessons.ParseToDtos();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LessonDto> GetUserCourseLesson(Guid userId, Guid courseId, int lessonId)
        {
            try
            {
                var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
                return userCourseLesson.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<LessonDto> UpdateUserCourseLessonName(Guid userId, Guid courseId, int lessonId, string lessonTitle)
        {
            try
            {
                var userCourseLesson = await _lessonRepository.GetUserCourseLessonById(userId, courseId, lessonId);
                userCourseLesson.Title = lessonTitle;
                await _lessonRepository.UpdateUserCourseLesson(userCourseLesson);
                return userCourseLesson.ParseToDto();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task CheckUserCourseExist(Guid userId, Guid courseId)
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

        private async Task CheckLessonTitle(string title)
        {
            try
            {
                await _lessonRepository.CheckLessonExistForTitle(title);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
