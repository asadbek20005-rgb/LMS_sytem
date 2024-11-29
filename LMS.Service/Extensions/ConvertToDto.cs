using LMS.Common.Dtos;
using LMS.Data.Entities;
using Mapster;

namespace LMS.Service.Extensions
{
    public static class ConvertToDto
    {
        public static UserDto ParseToDto(this User user)
        {
            return user.Adapt<UserDto>();
        }

        public static CourseDto ParseToDto(this Course course)
        {
            return course.Adapt<CourseDto>();
        }

        public static LessonDto ParseToDto(this Lesson lesson)
        {
            return lesson.Adapt<LessonDto>();
        }
        public static ContentDto ParseToDto(this Content content)
        {
            return content.Adapt<ContentDto>();
        }

        public static User_Course_PaymentDto ParseToDto(this User_Course_Payment user_Course_Payment)
        {
            return user_Course_Payment.Adapt<User_Course_PaymentDto>();
        }
        public static CardInfoDto ParseToDto(this CardInfo cardInfo)
        {
            return cardInfo.Adapt<CardInfoDto>();
        }
        public static List<UserDto> ParseToDtos(this List<User> users)
        {
            if (users is null || users.Count == 0) return [];

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(user.ParseToDto());
            }
            return userDtos;
        }

        public static List<CourseDto> ParseToDtos(this List<Course> courses)
        {
            if (courses.Count == 0 || courses is null) return [];

            var courseDtos = new List<CourseDto>();

            foreach (var course in courses)
            {
                courseDtos.Add(course.ParseToDto());
            }
            return courseDtos;
        }

        public static List<LessonDto> ParseToDtos(this List<Lesson> lessons)
        {
            if (lessons.Count == 0 || lessons is null) return [];
            var lessonDtos = new List<LessonDto>();
            foreach (var lesson in lessons)
            {
                lessonDtos.Add(lesson.ParseToDto());
            }
            return lessonDtos;
        }

        public static List<ContentDto> ParseToDtos(this List<Content> contents)
        {
            if (contents.Count == 0 || contents is null) return [];
            var contentDtos = new List<ContentDto>();
            foreach (var content in contents)
            {
                contentDtos.Add(content.ParseToDto());
            }
            return contentDtos;
        }
    }
}
